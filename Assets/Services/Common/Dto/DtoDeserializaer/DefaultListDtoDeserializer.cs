using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Services.Dto;

namespace Services.Common.Dto
{
    public class DefaultListDtoDeserializer<TResponse> where TResponse : ResponseBaseDto
    {
        private readonly IDtoDeserializer<TResponse> _itemDeserializer;

        public DefaultListDtoDeserializer(
            IDtoDeserializer<TResponse> deserializer = null)
        {
            _itemDeserializer = deserializer ?? new DefaultDtoDeserializer<TResponse>();
        }

        public List<TResponse> Deserialize(string json)
        {
            var jObject = JObject.Parse(json);
            var itemsArray = jObject["units"] as JArray;
            return itemsArray?.Select(jUnitObject => _itemDeserializer.Deserialize(jUnitObject.ToString())).ToList();
        }
    }
}