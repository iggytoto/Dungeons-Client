using System;
using Model.Units;

namespace Services.Common.Dto
{
    [Serializable]
    public class HumanArcherSkillsDto : SkillsDto
    {
        public long midRangePoints;
        public long longRangePoints;
        public bool fireArrows;
        public bool poisonArrows;


        public override Skills ToDomain()
        {
            return ToDomainTyped();
        }

        public HumanArcherSkills ToDomainTyped()
        {
            return new HumanArcherSkills
            {
                id = id,
                unitId = unitId,
                fireArrows = fireArrows,
                poisonArrows = poisonArrows,
                longRangePoints = longRangePoints,
                midRangePoints = midRangePoints
            };
        }
    }
}