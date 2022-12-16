using UnityEngine;

namespace Services
{
    public abstract class ServiceBase : MonoBehaviour, IService
    {
        protected string EndpointHttp;
        protected string EndpointHost;
        protected ushort EndpointPrt;

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
    }
}