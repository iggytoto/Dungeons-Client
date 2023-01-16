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
            object requestDto,
            string requestType,
            EventHandler<TResponse> successHandler,
            EventHandler<ErrorResponseDto> errorHandler,
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
            req.SetRequestHeader(Authorization, GetTokenValueHeader(_loginService.UserContext));

            yield return req.SendWebRequest();
            switch (req.result)
            {
                case UnityWebRequest.Result.InProgress:
                    break;
                case UnityWebRequest.Result.Success:
                    var responseString = System.Text.Encoding.UTF8.GetString(req.downloadHandler.data);
                    var response = dtoDeserializer.Deserialize(responseString);
                    if (response is
                        {
                            code: 0
                        })
                    {
                        successHandler?.Invoke(this, response);
                    }
                    else
                    {
                        errorHandler?.Invoke(this, new ErrorResponseDto { message = response?.message });
                    }

                    StopCoroutine(DoRequestCoroutine(url, requestBody, requestType, successHandler, errorHandler));
                    break;
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.ProtocolError:
                case UnityWebRequest.Result.DataProcessingError:
                    errorHandler.Invoke(this, new ErrorResponseDto { message = req.error });
                    StopCoroutine(DoRequestCoroutine(url, requestBody, requestType, successHandler, errorHandler));
                    break;
                default:
                    StopCoroutine(DoRequestCoroutine(url, requestBody, requestType, successHandler, errorHandler));
                    throw new ArgumentOutOfRangeException();
            }
        }

        public IEnumerator DoRequestCoroutine<TResponse>(
            string path,
            string requestBody,
            string requestType,
            EventHandler<TResponse> successHandler,
            EventHandler<ErrorResponseDto> errorHandler)
            where TResponse : ResponseBaseDto
        {
            return DoRequestCoroutine(
                path,
                requestBody,
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