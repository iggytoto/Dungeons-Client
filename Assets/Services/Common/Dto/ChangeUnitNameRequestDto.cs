using System;

namespace Services.Common.Dto
{
    [Serializable]
    public class ChangeUnitNameRequestDto
    {
        public long unitId;
        public string newName;
    }
}