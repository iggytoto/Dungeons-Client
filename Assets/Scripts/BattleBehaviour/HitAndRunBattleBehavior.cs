using System.Collections.Generic;
using UnityEngine.AI;

namespace DefaultNamespace.BattleBehaviour
{
    public class HitAndRunBattleBehavior : BattleBehaviorTree
    {
        protected override BattleBehaviorNode SetupTree()
        {
            var u = gameObject.GetComponent<UnitStateController>();
            var nma = gameObject.GetComponent<NavMeshAgent>();
            return new BattleBehaviorSequence(
                new List<BattleBehaviorNode>
                {
                    new SetClosestEnemyAsTargetTask(u),
                    new KeepTargetInRangeTask(u, nma),
                    new CheckIfTargetInAttackRangeTask(u),
                    new AttackBattleTask(u)
                }
            );
        }
    }
}