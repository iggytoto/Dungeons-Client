using System;
using Model.Units;

namespace DefaultNamespace.BattleBehaviour.Abilities
{
    public abstract class AbilityManager
    {
        public static BattleBehaviorNode GetAbilityFor(UnitStateController unitStateController)
        {
            return unitStateController.Unit.type switch
            {
                UnitType.HumanWarrior => new SpinToWinAbility(unitStateController),
                UnitType.Dummy => null,
                _ => null
            };
        }
    }
}