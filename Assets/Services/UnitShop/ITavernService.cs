using System;
using System.Collections.ObjectModel;
using DefaultNamespace;
using Model.Units;

namespace Services
{
    /**
     * Tavern provides functionality to hire units for a player.
     */
    public interface ITavernService : IUnitListProvider<UnitForSale>, IService
    {
        /**
         * List of currently available units for a player to hire
         */
        public ObservableCollection<UnitForSale> AvailableUnits { get; }

        /**
         * Command to hire given unit
         */
        public void BuyUnit(UnitType type, Action<Unit> onSuccess, Action<string> onError);

        ObservableCollection<UnitForSale> IUnitListProvider<UnitForSale>.Units => AvailableUnits;
    }
}