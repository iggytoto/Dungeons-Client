using Newtonsoft.Json.Linq;

namespace Services.Common.Dto
{
    public class HumanSpearmanEquipmentDtoDeserializer : UnitEquipmentDeserializerBase,
        IDtoDeserializer<HumanSpearmanSkillsDto>
    {
        public override SkillsDto Deserialize(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            var jObject = JObject.Parse(json);
            return new HumanSpearmanSkillsDto
            {
                id = jObject["id"]?.Value<long?>() ?? -1,
                unitId = jObject["unitId"]?.Value<long?>() ?? -1,
                midRangePoints = jObject["midRangePoints"]?.Value<long>() ?? -1,
                doubleEdgePoints = jObject["doubleEdgePoints"]?.Value<long>() ?? -1,
            };
        }

        HumanSpearmanSkillsDto IDtoDeserializer<HumanSpearmanSkillsDto>.Deserialize(string json)
        {
            return (HumanSpearmanSkillsDto)Deserialize(json);
        }
    }
}