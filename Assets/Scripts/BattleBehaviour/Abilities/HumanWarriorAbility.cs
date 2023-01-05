using DefaultNamespace.Animation;
using DefaultNamespace.UnitState;
using Model.Units;
using UnityEngine;

namespace DefaultNamespace.BattleBehaviour.Abilities
{
    public class HumanWarriorAbility : UnitTaskBase
    {
        private readonly float _duration = 5;
        private float _currentDuration;
        private bool _inProgress;
        private float _attackCoolDown;
        private readonly HumanWarriorEquipment _equipment;

        public HumanWarriorAbility(UnitStateController unitStateStateController) : base(unitStateStateController)
        {
            _equipment = (HumanWarriorEquipment)unitStateStateController.Equipment;
            if (_equipment != null)
            {
                _duration = 5 + (_equipment.defencePoints + _equipment.offencePoints - 2);
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
                return ProgressAbility();
            }

            if (UnitState.Mana >= UnitState.MaxMana)
            {
                return StartAbility();
            }

            State = BattleBehaviorNodeState.Failure;
            return State;
        }

        private BattleBehaviorNodeState ProgressAbility()
        {
            _attackCoolDown -= Time.deltaTime;
            _currentDuration += Time.deltaTime;
            if (_currentDuration >= _duration)
            {
                return FinishAbility();
            }

            if (_attackCoolDown <= 0)
            {
                foreach (var enemy in GetAllLiveEnemies())
                {
                    UnitState.DoAttack(enemy);
                }

                _attackCoolDown = 1 / UnitState.AttackSpeed;
            }

            State = BattleBehaviorNodeState.Running;
            return State;
        }

        private BattleBehaviorNodeState FinishAbility()
        {
            Animator.SetBool(AnimationConstants.IsAbilityBoolean, false);
            _inProgress = false;
            State = BattleBehaviorNodeState.Success;
            return State;
        }

        private BattleBehaviorNodeState StartAbility()
        {
            var protectionEffect = GetProtectionEffect();
            if (protectionEffect != null)
            {
                var e = UnitState.ApplyEffect<HumanWarriorAbilityProtectionEffect>();
                e.Init(_duration, protectionEffect.ARM, protectionEffect.Mr);
            }

            Animator.SetBool(AnimationConstants.IsAbilityBoolean, true);
            _inProgress = true;
            _currentDuration = 0;
            State = BattleBehaviorNodeState.Running;
            return State;
        }

        private ProtectionEffect GetProtectionEffect()
        {
            return _equipment.defencePoints switch
            {
                1 => new ProtectionEffect(5, 0),
                2 => new ProtectionEffect(10, 0),
                3 => new ProtectionEffect(15, 0),
                4 => new ProtectionEffect(20, 0),
                5 => new ProtectionEffect(20, 10),
                _ => null
            };
        }

        private class ProtectionEffect
        {
            public ProtectionEffect(long arm, long mr)
            {
                ARM = arm;
                Mr = mr;
            }

            public readonly long ARM;
            public readonly long Mr;
        }
    }
}