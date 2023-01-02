using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace.BattleBehaviour
{
    [RequireComponent(typeof(UnitStateController))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class AttackClosestEnemyBattleBehavior : BattleBehaviorTree
    {
        protected override BattleBehaviorNode SetupTree()
        {
            var u = gameObject.GetComponent<UnitStateController>();
            var nma = gameObject.GetComponent<NavMeshAgent>();
            return new BattleBehaviorSelector(
                new List<BattleBehaviorNode>
                {
                    new BattleBehaviorSequence(
                        new List<BattleBehaviorNode>
                        {
                            new CheckIfTargetInAttackRangeTask(u),
                            new AttackOrAbilityBattleTask(u)
                        }
                    ),
                    new MoveToTargetBattleTask(u, nma),
                    new SetClosestEnemyAsTargetTask(u),
                }
            );
        }
    }
}