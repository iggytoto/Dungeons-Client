using System;
using System.Collections;
using Newtonsoft.Json;
using Services.Common.Dto;
using UnityEngine;
using UnityEngine.Networking;

namespace Services.Dto
{
    public class ApiAdapter : MonoBehaviour
    {
        public const string Post = "POST";
        public const string Get = "GET";

        public string endpointHttp = "http";
        public string endpointAddress = "localhost";
        public ushort endpointPort = 8080;

        private const string ContentType = "Content-Type";
        private const string ApplicationJson = "application/json";
        private const string Authorization = "Authorization";
        private const string Bearer = "Bearer ";
        private ILoginService _loginService;

        private void Start()
        {
            _loginService = FindObjectOfType<GameService>().LoginService;
        }

        public IEnumerator DoRequestCoroutine<TResponse>(
            string path,
            RequestDto requestDto,
            string requestType,
            Action<TResponse> successHandler,
            Action<ErrorResponseDto> errorHandler,
            IDtoDeserializer<TResponse> dtoDeserializer)
            where TResponse : ResponseBaseDto
        {
            var url = GetConnectionAddress() + path;
            string requestBody = null;
            if (requestDto != null)
            {
                requestBody = SerializeDto(requestDto);
            }

            using var req = new UnityWebRequest(url, requestType);
            if (requestBody != null)
            {
                req.uploadHandler = new UploadHandlerRaw(new System.Text.UTF8Encoding().GetBytes(requestBody));
            }

            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader(ContentType, ApplicationJson);
            if (!url.Contains("auth"))
            {
                if (_loginService.UserContext == null)
                {
                    Debug.LogWarning("Request formed before user context is obtained");
                    yield return null;
                }
                else
                {
                    req.SetRequestHeader(Authorization, GetTokenValueHeader(_loginService.UserContext));
                }
            }

            yield return req.SendWebRequest();
            switch (req.result)
            {
                case UnityWebRequest.Result.InProgress:
                    break;
                case UnityWebRequest.Result.Success:
                    var responseString = req.downloadHandler.data == null
                        ? null
                        : System.Text.Encoding.UTF8.GetString(req.downloadHandler.data);
                    var response = dtoDeserializer.Deserialize(responseString);
                    if (response == null)
                    {
                        successHandler?.Invoke(null);
                    }
                    else if (response.code == 0)
                    {
                        successHandler?.Invoke(response);
                    }
                    else
                    {
                        errorHandler?.Invoke(new ErrorResponseDto { message = response.message });
                    }

                    break;
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.ProtocolError:
                case UnityWebRequest.Result.DataProcessingError:
                    errorHandler.Invoke(new ErrorResponseDto { message = req.error });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public IEnumerator DoRequestCoroutine<TResponse>(
            string path,
            RequestDto requestDto,
            string requestType,
            Action<TResponse> successHandler,
            Action<ErrorResponseDto> errorHandler)
            where TResponse : ResponseBaseDto
        {
            return DoRequestCoroutine(
                path,
                requestDto,
                requestType,
                successHandler,
                errorHandler,
                new DefaultDtoDeserializer<TResponse>());
        }

        protected string GetConnectionAddress()
        {
            return $"{endpointHttp}://{endpointAddress}:{endpointPort}";
        }

        public static string SerializeDto(object dto)
        {
            return JsonConvert.SerializeObject(dto);
        }

        private static string GetTokenValueHeader(UserContext ctx)
        {
            return Bearer + Convert.ToBase64String(
                System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(new TokenDto
                {
                    value = ctx.value,
                    userId = ctx.userId
                })));
        }
    }
}