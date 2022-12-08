using System;

namespace Services.Dto
{
    [Serializable]
    public class UnitDto
    {
        public long id;
        public string name;
        public long ownerId;
        public long hitPoints;
        public long armor;
        public long magicResistance;
        public long damage;
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
    }
}