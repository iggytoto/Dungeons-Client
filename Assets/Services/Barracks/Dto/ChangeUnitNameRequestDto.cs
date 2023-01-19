using System;

namespace Services.Common.Dto
{
    [Serializable]
    public class ChangeUnitNameRequestDto : RequestDto
    {
        public long unitId;
        public string newName;
    }
}