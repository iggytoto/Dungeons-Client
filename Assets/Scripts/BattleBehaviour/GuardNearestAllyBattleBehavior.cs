using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace.BattleBehaviour
{
    public class GuardNearestAllyBattleBehavior : BattleBehaviorTree
    {
        [SerializeField] [Range(0, 100)] private float allyToEnemyRadius;

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
                            new SetClosestEnemyAsTargetTask(u),
                            new CheckIfTargetInRadiusTargetAllyTask(u, allyToEnemyRadius),
                            new MoveToTargetBattleTask(u, nma),
                            new CheckIfTargetInAttackRangeTask(u),
                            new AttackOrAbilityBattleTask(u)
                        }
                    ),
                    new BattleBehaviorSequence(
                        new List<BattleBehaviorNode>
                        {
                            new SetClosestAllyAsTargetAllyTask(u),
                            new MoveToTargetAllyInRangeTask(u, nma, allyToEnemyRadius),
                        }
                    )
                }
            );
        }
    }
}