using System.Linq;
using UnityEngine;

namespace DefaultNamespace.BattleBehaviour
{
    public class SetClosestEnemyAsTargetTask : BattleBehaviorNode
    {
        private readonly UnitStateController _unit;

        public SetClosestEnemyAsTargetTask(UnitStateController unit)
        {
            _unit = unit;
        }

        public override BattleBehaviorNodeState Evaluate()
        {
            var closestTarget = Object.FindObjectsOfType<UnitStateController>()
                .Where(u => u.Unit.ownerId != _unit.Unit.ownerId && !u.IsDead())
                .OrderBy(u => Vector3.Distance(_unit.transform.position, u.transform.position))
                .FirstOrDefault();
            if (closestTarget == null)
            {
                State = BattleBehaviorNodeState.Failure;
                return State;
            }

            Parent.SetData("target", closestTarget);
            State = BattleBehaviorNodeState.Success;
            return State;
        }
    }
}