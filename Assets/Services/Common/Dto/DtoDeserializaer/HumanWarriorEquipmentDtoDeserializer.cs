using Newtonsoft.Json.Linq;

namespace Services.Common.Dto
{
    public class HumanWarriorEquipmentDtoDeserializer : UnitEquipmentDeserializerBase,
        IDtoDeserializer<HumanWarriorEquipmentDto>
    {
        public override EquipmentDto Deserialize(string json)
        {
            var jObject = JObject.Parse(json);
            return new HumanWarriorEquipmentDto
            {
                unitId = jObject["unitId"]?.Value<long>() ?? -1,
                code = jObject["code"]?.Value<long>() ?? -1,
                defencePoints = jObject["defencePoints"]?.Value<long>() ?? -1,
                offencePoints = jObject["offencePoints"]?.Value<long>() ?? -1
            };
        }

        HumanWarriorEquipmentDto IDtoDeserializer<HumanWarriorEquipmentDto>.Deserialize(string json)
        {
            return (HumanWarriorEquipmentDto)Deserialize(json);
        }
    }
}