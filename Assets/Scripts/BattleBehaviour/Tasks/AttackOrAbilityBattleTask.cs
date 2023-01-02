using System.Collections.Generic;
using DefaultNamespace.BattleBehaviour.Abilities;

namespace DefaultNamespace.BattleBehaviour
{
    public class AttackOrAbilityBattleTask : BattleBehaviorSelector
    {
        public AttackOrAbilityBattleTask(UnitStateController unitStateController) : base(
            new List<BattleBehaviorNode>
            {
                AbilityManager.GetAbilityFor(unitStateController),
                new AttackBattleTask(unitStateController)
            })
        {
        }
    }
}