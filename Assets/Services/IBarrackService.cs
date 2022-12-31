using System.Collections.ObjectModel;
using DefaultNamespace;
using Model.Units;

namespace Services
{
    /**
     * Barracks functions service
     */
    public interface IBarrackService : IUnitListProvider<Unit>, IService
    {
        /**
         * Collection of available units for player
         */
        public ObservableCollection<Unit> AvailableUnits { get; }


        /**
         * Sends command to the server to train unit with selected id
         */
        public void TrainUnit(long unitId);

        ObservableCollection<Unit> IUnitListProvider<Unit>.Units => AvailableUnits;
        void ChangeUnitName(long selectedUnitId, string newName);
        void ChangeUnitBattleBehavior(long selectedUnitId, BattleBehavior bb);
        void UpgradeUnitEquipment(long equipmentId, UnitType unitType, string upgradeParamName);
    }
}