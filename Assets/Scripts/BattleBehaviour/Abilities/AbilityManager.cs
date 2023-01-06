using BattleBehaviour.Abilities;
using Model.Units;

namespace DefaultNamespace.BattleBehaviour.Abilities
{
    public abstract class AbilityManager
    {
        public static BattleBehaviorNode GetAbilityFor(UnitStateController unitStateController)
        {
            return unitStateController.UnitType switch
            {
                UnitType.HumanWarrior => new HumanWarriorAbility(unitStateController),
                UnitType.HumanArcher => new HumanArcherAbility(unitStateController),
                UnitType.HumanSpearman => new HumanSpearmanAbility(unitStateController),
                UnitType.HumanCleric => new HumanClericAbility(unitStateController),
                UnitType.Dummy => null,
                _ => null
            };
        }
    }
}