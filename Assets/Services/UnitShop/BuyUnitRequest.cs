using System;
using Model.Units;

namespace Services.UnitShop
{
    [Serializable]
    public class BuyUnitRequest
    {
        public UnitType type;
    }
}