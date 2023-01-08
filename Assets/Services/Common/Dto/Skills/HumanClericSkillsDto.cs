using System;
using Model.Units;

namespace Services.Common.Dto
{
    [Serializable]
    public class HumanClericSkillsDto : SkillsDto
    {
        public long disciplinePoints;
        public bool shatter;
        public bool divine;
        public bool purge;


        public override Skills ToDomain()
        {
            return new HumanClericSkills
            {
                id = id,
                unitId = unitId,
                disciplinePoints = disciplinePoints,
                shatter = shatter,
                divine = divine,
                purge = purge
            };
        }
    }
}