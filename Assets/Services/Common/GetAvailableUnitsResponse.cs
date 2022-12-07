using System;
using System.Collections.Generic;
using Services.Dto;

namespace Services.UnitShop
{
    [Serializable]
    public class GetAvailableUnitsResponse : ResponseBase
    {
        public List<UnitDto> units;
    }
}