using Newtonsoft.Json;
using Services.Dto;

namespace Services.Common.Dto
{
    public class DefaultDtoDeserializer<TResponse> : IDtoDeserializer<TResponse> where TResponse : ResponseBaseDto
    {
        public TResponse Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<TResponse>(json);
        }
    }
}