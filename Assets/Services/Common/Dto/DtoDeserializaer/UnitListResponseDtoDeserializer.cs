using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Services.Common.Dto
{
    public class UnitListResponseDtoDeserializer : IDtoDeserializer<UnitListResponseDto>
    {
        private readonly IDtoDeserializer<UnitDto> _unitDeserializer = new UnitDtoDeserializer();

        public UnitListResponseDto Deserialize(string json)
        {
            var jObject = JObject.Parse(json);
            return new UnitListResponseDto
            {
                code = jObject["code"]?.Value<long>() ?? 0,
                message = jObject["message"]?.Value<string>(),
                items = ProcessUnits(jObject["items"] as JArray)
            };
        }

        private IEnumerable<UnitDto> ProcessUnits(JArray unitsArray)
        {
            return unitsArray?.Select(jUnitObject => _unitDeserializer.Deserialize(jUnitObject.ToString())).ToList();
        }
    }
}