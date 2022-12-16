using System;

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

        public Unit ToUnit()
        {
            return new Unit
            {
                Id = id,
                Name = name,
                Armor = armor,
                Damage = damage,
                HitPoints = hitPoints,
                MaxHp = maxHitPoints,
                AttackSpeed = attackSpeed,
                MagicResistance = magicResistance,
                OwnerId = ownerId,
                TrainingExperience = trainingExperience,
                Activity = activity
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
                armor = u.Armor,
                damage = u.Damage,
                hitPoints = u.HitPoints,
                maxHitPoints = u.MaxHp,
                attackSpeed = u.AttackSpeed,
                magicResistance = u.MagicResistance,
                ownerId = u.OwnerId,
                trainingExperience = u.TrainingExperience,
                activity = u.Activity
            };
        }
    }
}