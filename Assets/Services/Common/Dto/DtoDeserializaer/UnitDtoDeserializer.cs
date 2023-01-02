using System;
using Model.Units;
using Newtonsoft.Json.Linq;

namespace Services.Common.Dto
{
    public class UnitDtoDeserializer : IDtoDeserializer<UnitDto>
    {
        private readonly IDtoDeserializer<EquipmentDto> _equipDeserializer;

        public UnitDto Deserialize(string json)
        {
            var jObject = JObject.Parse(json);
            Enum.TryParse(jObject["unitType"]?.Value<string>(), out UnitType unitType);
            Enum.TryParse(jObject["activity"]?.Value<string>(), out Unit.UnitActivity unitActivity);
            Enum.TryParse(jObject["battleBehavior"]?.Value<string>(), out BattleBehavior battleBehavior);
            return new UnitDto
            {
                id = jObject["id"]?.Value<long>() ?? -1,
                name = jObject["name"]?.Value<string>(),
                ownerId = jObject["ownerId"]?.Value<long>() ?? -1,
                hitPoints = jObject["hitPoints"]?.Value<long>() ?? -1,
                maxHitPoints = jObject["maxHitPoints"]?.Value<long>() ?? -1,
                armor = jObject["armor"]?.Value<long>() ?? -1,
                magicResistance = jObject["magicResistance"]?.Value<long>() ?? -1,
                damage = jObject["damage"]?.Value<long>() ?? -1,
                attackSpeed = jObject["attackSpeed"]?.Value<float>() ?? -1,
                unitId = jObject["unitId"]?.Value<long>() ?? -1,
                goldAmount = jObject["goldAmount"]?.Value<long>() ?? -1,
                activity = unitActivity,
                attackRange = jObject["attackRange"]?.Value<float>() ?? -1,
                movementSpeed = jObject["movementSpeed"]?.Value<float>() ?? -1,
                battleBehavior = battleBehavior,
                unitType = unitType,
                startedAt = jObject["startedAt"]?.Value<DateTime>() ?? new DateTime(),
                unitEquip = UnitEquipmentDeserializerBase.GetDeserializer(unitType)
                    ?.Deserialize(jObject["unitEquip"]?.ToString())
            };
        }
    }
}