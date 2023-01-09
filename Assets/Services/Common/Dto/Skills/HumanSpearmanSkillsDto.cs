using System;
using Model.Units;

namespace Services.Common.Dto
{
    [Serializable]
    public class HumanSpearmanSkillsDto : SkillsDto
    {
        public long doubleEdgePoints;
        public long midRangePoints;

        public override Skills ToDomain()
        {
            return ToDomainTyped();
        }
        
        public HumanSpearmanSkills ToDomainTyped()
        {
            return new HumanSpearmanSkills()
            {
                id = id,
                unitId = unitId,
                doubleEdgePoints = doubleEdgePoints,
                midRangePoints = midRangePoints
            };
        }
    }
}