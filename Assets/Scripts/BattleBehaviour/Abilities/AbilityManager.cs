using BattleBehaviour.Abilities;
using Model.Units;

namespace DefaultNamespace.BattleBehaviour.Abilities
{
    public abstract class AbilityManager
    {
        public static BattleBehaviorNode GetAbilityFor(UnitStateController unitStateController)
        {
            return unitStateController.Unit.type switch
            {
                UnitType.HumanWarrior => new HumanWarriorAbility(unitStateController),
                UnitType.HumanArcher => new HumanArcherAbility(unitStateController),
                UnitType.Dummy => null,
                _ => null
            };
        }
    }
}