using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace.BattleBehaviour
{
    public abstract class UnitTaskBase : BattleBehaviorNode
    {
        protected const string TargetDataKey = "target";
        protected const string TargetAllyDataKey = "targetAlly";

        protected readonly Animator Animator;
        protected readonly UnitStateController UnitState;

        private readonly List<UnitStateController> _allUnits = new();

        protected UnitTaskBase(UnitStateController unitStateStateController)
        {
            UnitState = unitStateStateController;
            Animator = unitStateStateController.gameObject.GetComponentInChildren<Animator>();
        }

        protected UnitStateController GetTarget()
        {
            return (UnitStateController)GetData(TargetDataKey);
        }

        protected bool ClearTarget()
        {
            return ClearData(TargetDataKey);
        }

        public void SetTarget(UnitStateController target)
        {
            SetData(TargetDataKey, target);
        }

        protected UnitStateController GetTargetAlly()
        {
            return (UnitStateController)GetData(TargetAllyDataKey);
        }

        public override BattleBehaviorNodeState Evaluate()
        {
            if (UnitState == null || UnitState.IsDead())
            {
                State = BattleBehaviorNodeState.Failure;
                return State;
            }

            State = BattleBehaviorNodeState.Running;
            return State;
        }

        protected List<UnitStateController> GetAllUnits()
        {
            if (!_allUnits.Any())
            {
                _allUnits.AddRange(Object.FindObjectsOfType<UnitStateController>());
            }

            return _allUnits;
        }

        protected IEnumerable<UnitStateController> GetAllLiveEnemies()
        {
            return GetAllUnits().Where(u => u.TeamId != UnitState.TeamId && !u.IsDead()).ToList();
        }

        protected float GetAnimationTime()
        {
            return Animator.GetCurrentAnimatorClipInfo(1)[0].clip.averageDuration;
        }

        protected bool IsWithinAttackRange(UnitStateController target)
        {
            return GetDistanceToTarget(target) <= UnitState.AttackRange;
        }

        protected float GetDistanceToTarget(UnitStateController target)
        {
            var currentPosition = UnitState.transform.position;
            return Vector3.Distance(currentPosition, target.Collider.ClosestPointOnBounds(currentPosition));
        }
    }
}