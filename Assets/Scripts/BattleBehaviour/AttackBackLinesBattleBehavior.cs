using System.Collections.Generic;
using UnityEngine.AI;

namespace DefaultNamespace.BattleBehaviour
{
    public class AttackBackLinesBattleBehavior : BattleBehaviorTree
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
                            new AttackBattleTask(u)
                        }
                    ),
                    new MoveToTargetBattleTask(u, nma),
                    new SetFarthestEnemyFromAllyTeamTask(u),
                }
            );
        }
    }
}