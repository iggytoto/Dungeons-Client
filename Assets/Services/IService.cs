namespace Services
{
    public interface IService
    {
        public string EndpointHttpType { set; }
        public string EndpointAddress { set; }
        public ushort EndpointPort { set; }
        public void InitService();
    }
}