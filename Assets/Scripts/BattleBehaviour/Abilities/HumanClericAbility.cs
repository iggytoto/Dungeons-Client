using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Animation;
using DefaultNamespace.BattleBehaviour;
using DefaultNamespace.UnitState;
using Model.Damage;
using Model.Units;
using UnityEngine;

namespace BattleBehaviour.Abilities
{
    public class HumanClericAbility : UnitTaskBase
    {
        public HumanClericAbility(UnitStateController unitStateStateController) : base(unitStateStateController)
        {
        }

        public override BattleBehaviorNodeState Evaluate()
        {
            if (base.Evaluate() == BattleBehaviorNodeState.Failure)
            {
                return BattleBehaviorNodeState.Failure;
            }

            if (UnitState.Mana >= UnitState.MaxMana)
            {
                var abilityParams = GetAbilityParams();
                var targets = GetAllUnits().Where(u => !u.IsDead()).Take(abilityParams.NumberOfTargets).ToList();
                if (targets.Count == 0)
                {
                    State = BattleBehaviorNodeState.Failure;
                    return State;
                }

                UnitState.Mana = 0;
                Animator.SetTrigger(AnimationConstants.AttackTrigger);
                UnitState.StartCoroutine(DelayedAttack(targets, GetAnimationTime()));
                State = BattleBehaviorNodeState.Success;
                return State;
            }

            State = BattleBehaviorNodeState.Failure;
            return State;
        }

        private IEnumerator DelayedAttack(List<UnitStateController> targets, float animationTime)
        {
            yield return new WaitForSeconds(animationTime);
            var abilityParams = GetAbilityParams();
            foreach (var target in targets)
            {
                var isEnemy = target.OwnerId != UnitState.OwnerId;
                if (isEnemy)
                {
                    var damageValue = UnitState.AttackDamage;
                    if (abilityParams.Divine)
                    {
                        damageValue += (abilityParams.NumberOfTargets - 1) * 5;
                    }

                    target.DoAttack(target, true, Damage.Magic(damageValue), (t) =>
                    {
                        if (abilityParams.Shatter)
                        {
                            var e = t.ApplyEffect<HumanClericShatterEffect>();
                            e.Init(abilityParams.NumberOfTargets, -25);
                        }

                        if (abilityParams.Divine)
                        {
                            t.ClearPositiveEffects();
                        }
                    });
                }
                else
                {
                    var healValue = UnitState.AttackDamage;
                    if (abilityParams.Divine)
                    {
                        healValue += (abilityParams.NumberOfTargets - 1) * 5;
                    }

                    target.DoAttack(target, true, Damage.Magic(0), (t) =>
                    {
                        t.Heal(healValue);
                        if (abilityParams.Shatter)
                        {
                            var e = t.ApplyEffect<HumanClericShatterEffect>();
                            e.Init(abilityParams.NumberOfTargets, 25);
                        }

                        if (abilityParams.Divine)
                        {
                            t.ClearNegativeEffects();
                        }
                    });
                }
            }
        }

        private MulticastParams GetAbilityParams()
        {
            var eq = GetEquipment();
            return eq == null
                ? new MulticastParams(1, false, false, false)
                : new MulticastParams((int)(1 + eq.disciplinePoints), eq.shatter, eq.divine, eq.purge);
        }

        private HumanClericEquipment GetEquipment()
        {
            return (HumanClericEquipment)UnitState.Equipment;
        }

        private class MulticastParams
        {
            public int NumberOfTargets;
            public bool Shatter;
            public bool Divine;
            public bool Purge;

            public MulticastParams(int t, bool s, bool d, bool p)
            {
                NumberOfTargets = t;
                Shatter = s;
                Divine = d;
                Purge = p;
            }
        }
    }
}