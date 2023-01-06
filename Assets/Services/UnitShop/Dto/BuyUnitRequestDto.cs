using System;
using Model.Units;

namespace Services.UnitShop
{
    [Serializable]
    public class BuyUnitRequestDto
    {
        public UnitType type;
    }
}