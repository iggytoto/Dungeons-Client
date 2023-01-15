using System;
using System.Collections.Generic;
using Services.Dto;

namespace Services.Common.Dto
{
    [Serializable]
    public class UnitListResponseDto : ResponseBaseDto
    {
        public IEnumerable<UnitDto> items;
    }
}