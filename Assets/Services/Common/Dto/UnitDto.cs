using System;
using Model.Units;

namespace Services.Common.Dto
{
    [Serializable]
    public class UnitDto
    {
        public long id;
        public string name;
        public long ownerId;
        public long hitPoints;
        public long maxHitPoints;
        public long armor;
        public long magicResistance;
        public long damage;
        public float attackSpeed;
        public long trainingExperience;
        public long unitId;
        public long goldAmount;
        public Unit.UnitActivity activity;
        public float attackRange;
        public float movementSpeed;
        public BattleBehavior battleBehavior;
        public UnitType unitType;
        public DateTime startedAt;

        public Unit ToUnit()
        {
            return new Unit
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
                trainingExperience = trainingExperience,
                activity = activity,
                type = unitType,
                attackRange = attackRange,
                battleBehavior = battleBehavior,
                movementSpeed = movementSpeed
            };
        }

        public UnitForSale ToUnitForSale()
        {
            return UnitForSale.Of(ToUnit(), goldAmount);
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
                attackSpeed = u.attackSpeed,
                magicResistance = u.magicResistance,
                ownerId = u.ownerId,
                trainingExperience = u.trainingExperience,
                activity = u.activity,
                attackRange = u.attackRange,
                battleBehavior = u.battleBehavior,
                movementSpeed = u.movementSpeed,
                unitType = u.type
            };
        }
    }
}