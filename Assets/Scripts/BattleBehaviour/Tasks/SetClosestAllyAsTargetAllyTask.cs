using System.Linq;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace DefaultNamespace.BattleBehaviour
{
    public class SetClosestAllyAsTargetAllyTask : UnitTaskBase
    {
        public SetClosestAllyAsTargetAllyTask(UnitStateController unit) : base(unit)
        {
        }

        public override BattleBehaviorNode.BattleBehaviorNodeState Evaluate()
        {
            var closestTarget = Object.FindObjectsOfType<UnitStateController>()
                .Where(u => u.Unit.ownerId == Unit.Unit.ownerId && u.Unit.Id != Unit.Unit.Id && !u.IsDead())
                .OrderBy(u => Vector3.Distance(Unit.transform.position, u.transform.position))
                .FirstOrDefault();
            if (closestTarget == null)
            {
                State = BattleBehaviorNodeState.Failure;
                return State;
            }

            SetData(TargetAllyDataKey, closestTarget);
            State = BattleBehaviorNodeState.Success;
            return State;
        }
    }
}