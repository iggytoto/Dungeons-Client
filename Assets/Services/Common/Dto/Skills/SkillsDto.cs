using System;
using Model.Units;
using Services.Dto;

namespace Services.Common.Dto
{
    [Serializable]
    public abstract class SkillsDto : ResponseBaseDto
    {
        public long id;
        public long unitId;

        public abstract Skills ToDomain();

    }
}