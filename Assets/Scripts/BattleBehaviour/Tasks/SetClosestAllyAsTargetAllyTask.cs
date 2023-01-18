using System.Linq;
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
            var closestTarget = GetAllUnits()
                .Where(u => u.TeamId == UnitState.TeamId && u.Id != UnitState.Id && !u.IsDead())
                .OrderBy(u => Vector3.Distance(UnitState.transform.position, u.transform.position))
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