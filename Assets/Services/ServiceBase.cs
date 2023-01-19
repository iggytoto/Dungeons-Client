using System;
using Services.Dto;
using UnityEngine;

namespace Services
{
    public abstract class ServiceBase : MonoBehaviour, IService
    {
        protected string EndpointHttp;
        protected string EndpointHost;
        protected ushort EndpointPrt;
        protected ApiAdapter APIAdapter;

        public string EndpointHttpType
        {
            set => EndpointHttp = value;
        }

        public string EndpointAddress
        {
            set => EndpointHost = value;
        }

        public ushort EndpointPort
        {
            set => EndpointPrt = value;
        }

        public void InitService()
        {
            APIAdapter = gameObject.AddComponent<ApiAdapter>();
            APIAdapter.endpointHttp = EndpointHttp;
            APIAdapter.endpointAddress = EndpointHost;
            APIAdapter.endpointPort = EndpointPrt;
        }
    }
}