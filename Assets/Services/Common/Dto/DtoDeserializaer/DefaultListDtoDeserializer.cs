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
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            if (string.Equals("[]", json))
            {
                return new List<TResponse>();
            }

            var itemsArray = JArray.Parse(json);
            return itemsArray.Select(jUnitObject => _itemDeserializer.Deserialize(jUnitObject.ToString())).ToList();
        }
    }
}