using System;
using Model.Units;
using Newtonsoft.Json.Linq;
using Services.Common.Dto.Items;

namespace Services.Common.Dto
{
    public class UnitDtoDeserializer : IDtoDeserializer<UnitDto>
    {
        private readonly IDtoDeserializer<SkillsDto> _equipDeserializer;
        private readonly DefaultListDtoDeserializer<ItemDto> _itemDtoListDeserializer = new();

        public UnitDto Deserialize(string json)
        {
            var jObject = JObject.Parse(json);
            Enum.TryParse(jObject["unitType"]?.Value<string>(), out UnitType unitType);
            Enum.TryParse(jObject["activity"]?.Value<string>(), out UnitActivity unitActivity);
            Enum.TryParse(jObject["battleBehavior"]?.Value<string>(), out BattleBehavior battleBehavior);
            var id = jObject["id"]?.Value<long?>() ?? -1;
            var name = jObject["name"]?.Value<string>();
            var ownerId = jObject["ownerId"]?.Value<long?>() ?? -1;
            var hitPoints = jObject["hitPoints"]?.Value<long>() ?? -1;
            var maxHitPoints = jObject["maxHitPoints"]?.Value<long>() ?? -1;
            var armor = jObject["armor"]?.Value<long>() ?? -1;
            var magicResistance = jObject["magicResistance"]?.Value<long>() ?? -1;
            var damage = jObject["damage"]?.Value<long>() ?? -1;
            var attackSpeed = jObject["attackSpeed"]?.Value<float>() ?? -1;
            var unitId = jObject["unitId"]?.Value<long>() ?? -1;
            var goldAmount = jObject["goldAmount"]?.Value<long>() ?? -1;
            var attackRange = jObject["attackRange"]?.Value<float>() ?? -1;
            var movementSpeed = jObject["movementSpeed"]?.Value<float>() ?? -1;
            var mana = jObject["mana"]?.Value<long>() ?? -1;
            var maxMana = jObject["maxMana"]?.Value<long>() ?? -1;
            var startedAt = jObject["startedAt"]?.Value<DateTime>() ?? new DateTime();
            var unitEquipJson = jObject["unitEquip"]?.ToString();
            var unitEquip = string.IsNullOrEmpty(unitEquipJson)
                ? null
                : UnitSkillsDeserializerBase.GetDeserializer(unitType)
                    ?.Deserialize(unitEquipJson);
            var itemsDto = _itemDtoListDeserializer.Deserialize(jObject["items"]?.ToString());
            return new UnitDto
            {
                id = id,
                name = name,
                ownerId = ownerId,
                hitPoints = hitPoints,
                maxHitPoints = maxHitPoints,
                armor = armor,
                magicResistance = magicResistance,
                damage = damage,
                attackSpeed = attackSpeed,
                unitId = unitId,
                goldAmount = goldAmount,
                activity = unitActivity,
                attackRange = attackRange,
                movementSpeed = movementSpeed,
                battleBehavior = battleBehavior,
                unitType = unitType,
                mana = mana,
                maxMana = maxMana,
                startedAt = startedAt,
                skills = unitEquip,
                items = itemsDto
            };
        }
    }
}