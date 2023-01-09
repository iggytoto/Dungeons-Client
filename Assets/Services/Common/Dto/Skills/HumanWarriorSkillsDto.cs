using System;
using Model.Units;

namespace Services.Common.Dto
{
    [Serializable]
    public class HumanWarriorSkillsDto : SkillsDto
    {
        public long defencePoints;
        public long offencePoints;

        public override Skills ToDomain()
        {
            return ToDomainTyped();
        }

        public HumanWarriorSkills ToDomainTyped()
        {
            return new HumanWarriorSkills
            {
                id = id,
                defencePoints = defencePoints,
                offencePoints = offencePoints,
                unitId = unitId
            };
        }
    }
}