using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace.BattleBehaviour
{
    public class SetFarthestEnemyFromAllyTeamTask : UnitTaskBase
    {
        public SetFarthestEnemyFromAllyTeamTask(UnitStateController unitStateStateController) : base(unitStateStateController)
        {
        }

        public override BattleBehaviorNodeState Evaluate()
        {
            var allUnits = GetAllUnits();
            var allies = allUnits.Where(u => u.OwnerId == UnitState.OwnerId && !u.IsDead());
            var center = GetCenter(allies.Select(u => u.transform.position));
            var closestTarget = allUnits
                .Where(u => u.OwnerId != UnitState.OwnerId && !u.IsDead())
                .OrderByDescending(u => Vector3.Distance(center, u.transform.position))
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

        private static Vector3 GetCenter(IEnumerable<Vector3> positions)
        {
            var enumerable = positions as Vector3[] ?? positions.ToArray();
            var count = enumerable.Length;
            switch (count)
            {
                case 0:
                    return Vector3.zero;
                case 1:
                    return enumerable.First();
                default:
                {
                    var bounds = new Bounds(enumerable[0], Vector3.zero);
                    for (var i = 1; i < enumerable.Length; i++)
                    {
                        bounds.Encapsulate(enumerable[i]);
                    }

                    return bounds.center;
                }
            }
        }
    }
}