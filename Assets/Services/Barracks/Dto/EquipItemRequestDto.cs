using System;

namespace Services.Common.Dto
{
    [Serializable]
    public class EquipItemRequestDto : RequestDto
    {
        public long itemId;
        public long unitId;
    }
}