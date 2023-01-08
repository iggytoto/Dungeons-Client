using System.Linq;
using DefaultNamespace.Animation;
using DefaultNamespace.UnitState;
using Model.Damage;
using Model.Units;
using UnityEngine;

namespace DefaultNamespace.BattleBehaviour.Abilities
{
    public class HumanWarriorAbility : UnitTaskBase
    {
        private float _currentDuration;
        private bool _inProgress;
        private float _attackCoolDown;
        private HumanWarriorSkills _skills;

        public HumanWarriorAbility(UnitStateController unitStateStateController) : base(unitStateStateController)
        {
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
            if (_currentDuration >= GetDuration())
            {
                return FinishAbility();
            }

            if (_attackCoolDown <= 0)
            {
                foreach (var enemy in GetAllLiveEnemies().Where(e =>
                             Vector3.Distance(e.transform.position, UnitState.transform.position) <=
                             UnitState.AttackRange))
                {
                    enemy.DoDamage(Damage.Physical(UnitState.AttackDamage));
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
            UnitState.Mana = 0;
            var protectionEffect = GetProtectionEffect();
            if (protectionEffect != null)
            {
                var e = UnitState.ApplyEffect<HumanWarriorAbilityProtectionEffect>();
                e.Init(GetDuration(), protectionEffect.ARM, protectionEffect.Mr);
            }

            Animator.SetBool(AnimationConstants.IsAbilityBoolean, true);
            _inProgress = true;
            _currentDuration = 0;
            State = BattleBehaviorNodeState.Running;
            return State;
        }

        private ProtectionEffect GetProtectionEffect()
        {
            var eq = (HumanWarriorSkills)UnitState.Skills;
            return eq.defencePoints switch
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

        private float GetDuration()
        {
            var eq = GetEquipment();
            if (eq != null)
            {
                return 5 + (eq.defencePoints + eq.offencePoints - 2);
            }

            return 4;
        }

        private HumanWarriorSkills GetEquipment()
        {
            return _skills ??= (HumanWarriorSkills)UnitState.Skills;
        }
    }
}