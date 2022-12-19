using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Services.Dto
{
    public class ApiAdapterBase : MonoBehaviour
    {
        public string endpointHttp = "http";
        public string endpointAddress = "localhost";
        public ushort endpointPort = 8080;
        private const string ContentType = "Content-Type";
        private const string ApplicationJson = "application/json";
        protected const string Post = "POST";
        protected const string Get = "GET";
        protected const string Authorization = "Authorization";
        private const string Bearer = "Bearer ";

        protected IEnumerator DoRequestCoroutine<TResponse>(
            string url,
            string requestBody,
            string requestType,
            EventHandler<TResponse> successHandler,
            EventHandler<ErrorResponseDto> errorHandler)
            where TResponse : ResponseBaseDto
        {
            return DoRequestCoroutine(url, requestBody, requestType, null, successHandler, errorHandler);
        }

        protected IEnumerator DoRequestCoroutine<TResponse>(
            string url,
            string requestBody,
            string requestType,
            Dictionary<string, string> headers,
            EventHandler<TResponse> successHandler,
            EventHandler<ErrorResponseDto> errorHandler)
            where TResponse : ResponseBaseDto
        {
            using var req = new UnityWebRequest(url, requestType);
            if (requestType != Get && requestBody != null)
            {
                req.uploadHandler = new UploadHandlerRaw(new System.Text.UTF8Encoding().GetBytes(requestBody));
            }

            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader(ContentType, ApplicationJson);
            if (headers is { Count: > 0 })
            {
                foreach (var keyValuePair in headers)
                {
                    req.SetRequestHeader(keyValuePair.Key, keyValuePair.Value);
                }
            }

            yield return req.SendWebRequest();
            switch (req.result)
            {
                case UnityWebRequest.Result.InProgress:
                    break;
                case UnityWebRequest.Result.Success:
                    var response =
                        JsonUtility.FromJson<TResponse>(System.Text.Encoding.UTF8.GetString(req.downloadHandler.data));
                    if (response.code == 0)
                    {
                        successHandler?.Invoke(this, response);
                    }
                    else
                    {
                        errorHandler?.Invoke(this, new ErrorResponseDto { message = response.message });
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

        protected static string GetTokenValueHeader(UserContext ctx)
        {
            return Bearer + Convert.ToBase64String(
                System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(new TokenDto
                {
                    value = ctx.value,
                    userId = ctx.userId
                })));
        }

        public string GetConnectionAddress()
        {
            return $"{endpointHttp}://{endpointAddress}:{endpointPort}";
        }
    }
}