using Newtonsoft.Json.Linq;

namespace Services.Common.Dto
{
    public class HumanArcherEquipmentDtoDeserializer : UnitEquipmentDeserializerBase,
        IDtoDeserializer<HumanArcherEquipmentDto>
    {
        public override EquipmentDto Deserialize(string json)
        {
            var jObject = JObject.Parse(json);
            return new HumanArcherEquipmentDto
            {
                unitId = jObject["unitId"]?.Value<long>() ?? -1,
                code = jObject["code"]?.Value<long>() ?? -1,
                midRangePoints = jObject["midRangePoints"]?.Value<long>() ?? -1,
                longRangePoints = jObject["longRangePoints"]?.Value<long>() ?? -1,
                fireArrows = jObject["fireArrows"]?.Value<bool>() ?? false,
                poisonArrows = jObject["poisonArrows"]?.Value<bool>() ?? false
            };
        }

        HumanArcherEquipmentDto IDtoDeserializer<HumanArcherEquipmentDto>.Deserialize(string json)
        {
            return (HumanArcherEquipmentDto)Deserialize(json);
        }
    }
}