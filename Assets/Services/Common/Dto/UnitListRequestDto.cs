using System;
using System.Collections.Generic;
using Services.Common.Dto;
using Services.Dto;

namespace Services.Common
{
    [Serializable]
    public class UnitListRequestDto
    {
        public List<UnitDto> units;
    }
}