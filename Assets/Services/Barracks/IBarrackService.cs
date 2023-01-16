using System;
using System.Collections.ObjectModel;
using DefaultNamespace;
using Model.Items;
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
         * Collection of available unequipped items for player
         */
        public ObservableCollection<Item> AvailableItems { get; }

        ObservableCollection<Unit> IUnitListProvider<Unit>.Units => AvailableUnits;
        void ChangeUnitName(long selectedUnitId, string newName);
        void ChangeUnitBattleBehavior(long selectedUnitId, BattleBehavior bb);
        void EquipItem(Item item, Unit unit);
        void UnEquipItem(Item item);

        /**
         * Request an upgrade for given skill by id.
         *
         * <param name="skillId"> skills id to upgrade</param>
         * <param name="unitType"> unit type of skill's unit</param>
         * <param name="upgradeParamName"> skill parameter name to upgrade</param>
         * <param name="onSuccess"> success handler</param>
         * <param name="dtoMapper"> dto to domain mapper</param>
         */
        public void UpgradeUnitSkill<TDomain, TDto>(
            long skillId,
            UnitType unitType,
            string upgradeParamName,
            EventHandler<TDomain> onSuccess,
            Func<TDto, TDomain> dtoMapper)
            where TDomain : Skills
            where TDto : SkillsDto;
    }
}