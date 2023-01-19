using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Services.Dto;

namespace Services.Common.Dto
{
    public class DefaultListResponseDtoDeserializer<TDto> : IDtoDeserializer<ListResponseDto<TDto>>
        where TDto : ResponseBaseDto
    {
        private readonly IDtoDeserializer<TDto> _dtoDeserializer;

        public DefaultListResponseDtoDeserializer(IDtoDeserializer<TDto> dtoDeserializer)
        {
            _dtoDeserializer = dtoDeserializer;
        }

        public ListResponseDto<TDto> Deserialize(string json)
        {
            var jObject = JObject.Parse(json);
            return new ListResponseDto<TDto>
            {
                code = jObject["code"]?.Value<long>() ?? 0,
                message = jObject["message"]?.Value<string>(),
                items = ProcessUnits(jObject["items"] as JArray)
            };
        }

        private List<TDto> ProcessUnits(JArray unitsArray)
        {
            return unitsArray?.Select(jUnitObject => _dtoDeserializer.Deserialize(jUnitObject.ToString())).ToList();
        }
    }
}