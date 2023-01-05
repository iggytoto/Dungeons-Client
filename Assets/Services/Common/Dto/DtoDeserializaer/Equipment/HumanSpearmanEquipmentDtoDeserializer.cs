using Newtonsoft.Json.Linq;

namespace Services.Common.Dto
{
    public class HumanSpearmanEquipmentDtoDeserializer : UnitEquipmentDeserializerBase,
        IDtoDeserializer<HumanSpearmanEquipmentDto>
    {
        public override EquipmentDto Deserialize(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            var jObject = JObject.Parse(json);
            return new HumanSpearmanEquipmentDto
            {
                id = jObject["id"]?.Value<long?>() ?? -1,
                unitId = jObject["unitId"]?.Value<long?>() ?? -1,
                midRangePoints = jObject["midRangePoints"]?.Value<long>() ?? -1,
                doubleEdgePoints = jObject["doubleEdgePoints"]?.Value<long>() ?? -1,
            };
        }

        HumanSpearmanEquipmentDto IDtoDeserializer<HumanSpearmanEquipmentDto>.Deserialize(string json)
        {
            return (HumanSpearmanEquipmentDto)Deserialize(json);
        }
    }
}