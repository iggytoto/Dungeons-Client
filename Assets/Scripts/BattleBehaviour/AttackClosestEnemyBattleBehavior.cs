using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.BattleBehaviour
{
    [RequireComponent(typeof(UnitStateController))]
    public class AttackClosestEnemyBattleBehavior : BattleBehaviorTree
    {
        protected override BattleBehaviorNode SetupTree()
        {
            var u = gameObject.GetComponent<UnitStateController>();
            return new BattleBehaviorSelector(
                new List<BattleBehaviorNode>
                {
                    new BattleBehaviorSequence(
                        new List<BattleBehaviorNode>
                        {
                            new CheckIfTargetInAttackRangeTask(u),
                            new AttackBattleTask(u)
                        }
                    ),
                    new MoveToTargetBattleTask(u),
                    new SetClosestEnemyAsTargetTask(u),
                }
            );
        }
    }
}