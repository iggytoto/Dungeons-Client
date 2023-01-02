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
                UnitType.HumanWarrior => new BattleBehaviorNode(),
                UnitType.Dummy => new BattleBehaviorNode(),
                _ => new BattleBehaviorNode()
            };
        }
    }
}