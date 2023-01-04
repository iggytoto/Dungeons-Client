using System.Linq;
using UnityEngine;

namespace DefaultNamespace.BattleBehaviour
{
    public class SetClosestEnemyAsTargetTask : UnitTaskBase
    {
        public SetClosestEnemyAsTargetTask(UnitStateController unit) : base(unit)
        {
        }

        public override BattleBehaviorNodeState Evaluate()
        {
            var closestTarget = GetAllUnits()
                .Where(u => u.Unit.ownerId != UnitState.Unit.ownerId && !u.IsDead())
                .OrderBy(u => Vector3.Distance(UnitState.transform.position, u.transform.position))
                .FirstOrDefault();
            if (closestTarget == null)
            {
                State = BattleBehaviorNodeState.Failure;
                return State;
            }

            Parent.SetData(TargetDataKey, closestTarget);
            State = BattleBehaviorNodeState.Success;
            return State;
        }
    }
}