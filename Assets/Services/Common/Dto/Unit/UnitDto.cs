using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Model.Units;
using Services.Common.Dto.Items;
using Services.Dto;

namespace Services.Common.Dto
{
    [Serializable]
    public class UnitDto : ResponseBaseDto
    {
        public long id;
        [CanBeNull] public string name;
        public long ownerId;
        public long hitPoints;
        public long maxHitPoints;
        public long mana;
        public long maxMana;
        public long armor;
        public long magicResistance;
        public long damage;
        public float attackSpeed;
        public long unitId;
        public long goldAmount;
        public UnitActivity activity;
        public float attackRange;
        public float movementSpeed;
        public BattleBehavior battleBehavior;
        public UnitType unitType;
        public DateTime startedAt;
        [CanBeNull] public SkillsDto skills;
        [CanBeNull] public List<ItemDto> items;

        public Unit ToDomain()
        {
            var result = new Unit
            {
                Id = id,
                Name = name,
                armor = armor,
                damage = damage,
                hitPoints = hitPoints,
                maxHp = maxHitPoints,
                attackSpeed = attackSpeed,
                magicResistance = magicResistance,
                ownerId = ownerId,
                activity = activity,
                type = unitType,
                attackRange = attackRange,
                battleBehavior = battleBehavior,
                movementSpeed = movementSpeed,
                skills = skills?.ToDomain(),
            };
            var itms = items?.Select(i => i.ToDomain()).ToList();
            if (itms != null)
            {
                result.items.AddRange(itms);
            }

            return result;
        }

        public UnitForSale ToUnitForSale()
        {
            return UnitForSale.Of(ToDomain(), goldAmount);
        }

        public static UnitDto Of(Unit u)
        {
            return new UnitDto
            {
                id = u.Id,
                name = u.Name,
                armor = u.armor,
                damage = u.damage,
                hitPoints = u.hitPoints,
                maxHitPoints = u.maxHp,
                mana = u.mana,
                maxMana = u.maxMana,
                attackSpeed = u.attackSpeed,
                magicResistance = u.magicResistance,
                ownerId = u.ownerId,
                activity = u.activity,
                attackRange = u.attackRange,
                battleBehavior = u.battleBehavior,
                movementSpeed = u.movementSpeed,
                unitType = u.type
            };
        }
    }
}