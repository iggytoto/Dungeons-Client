using DefaultNamespace.Animation;
using Model.Units;
using UnityEngine;

namespace DefaultNamespace.BattleBehaviour.Abilities
{
    public class SpinToWinAbility : UnitTaskBase
    {
        private readonly float _duration = 5;
        private float _currentDuration;
        private bool _inProgress;
        private float _attackCoolDown;

        public SpinToWinAbility(UnitStateController unitStateController) : base(unitStateController)
        {
            var equip = (HumanWarriorEquipment)unitStateController.Unit.equip;
            if (equip != null)
            {
                _duration = 5 + (equip.defencePoints + equip.offencePoints - 2);
            }
        }

        public override BattleBehaviorNodeState Evaluate()
        {
            if (base.Evaluate() == BattleBehaviorNodeState.Failure)
            {
                return BattleBehaviorNodeState.Failure;
            }

            if (_inProgress)
            {
                _attackCoolDown -= Time.deltaTime;
                _currentDuration += Time.deltaTime;
                if (_currentDuration >= _duration)
                {
                    Animator.SetBool(AnimationConstants.IsAbilityBoolean, false);
                    _inProgress = false;
                    State = BattleBehaviorNodeState.Success;
                    return State;
                }

                if (_attackCoolDown <= 0)
                {
                    foreach (var enemy in GetAllLiveEnemies())
                    {
                        Unit.DoAttack(enemy);
                    }

                    _attackCoolDown = 1 / Unit.GetCurrentAttackSpeed();
                }

                State = BattleBehaviorNodeState.Running;
                return State;
            }

            if (Unit.GetCurrentMana() >= Unit.GetMaxMana())
            {
                Animator.SetBool(AnimationConstants.IsAbilityBoolean, true);
                _inProgress = true;
                _currentDuration = 0;
                State = BattleBehaviorNodeState.Running;
                return State;
            }

            State = BattleBehaviorNodeState.Failure;
            return State;
        }
    }
}