using System;
using System.Collections.ObjectModel;
using DefaultNamespace;
using Model.Units;
using Services.Common.Dto;

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

        /**
         * Request an upgrade for given equipment by id.
         *
         * <param name="equipmentId"> equipment id to upgrade</param>
         * <param name="unitType"> unit type of equipment's unit</param>
         * <param name="upgradeParamName"> equipment parameter name to upgrade</param>
         * <param name="onSuccess"> success handler</param>
         * <param name="dtoMapper"> dto to domain mapper</param>
         */
        public void UpgradeUnitEquipment<TDomain, TDto>(
            long equipmentId,
            UnitType unitType,
            string upgradeParamName,
            EventHandler<TDomain> onSuccess,
            Func<TDto, TDomain> dtoMapper)
            where TDomain : Equipment
            where TDto : EquipmentDto;
    }
}