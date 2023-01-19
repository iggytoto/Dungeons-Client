using System;
using Model.Units;
using Services.Common.Dto;

namespace Services.UnitShop
{
    [Serializable]
    public class BuyUnitRequestDto : RequestDto
    {
        public UnitType type;
    }
}