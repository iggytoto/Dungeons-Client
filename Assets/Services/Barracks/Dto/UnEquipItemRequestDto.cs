using System;

namespace Services.Common.Dto
{
    [Serializable]
    public class UnEquipItemRequestDto : RequestDto
    {
        public long itemId;
    }
}