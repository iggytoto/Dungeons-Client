using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Animation;
using DefaultNamespace.BattleBehaviour;
using Model.Units;
using UnityEngine;
using Random = System.Random;

namespace BattleBehaviour.Abilities
{
    public class HumanSpearmanAbility : UnitTaskBase
    {
        public HumanSpearmanAbility(UnitStateController unitStateStateController) : base(unitStateStateController)
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
                var targets = GetAllLiveEnemies().Where(e =>
                        Vector3.Distance(UnitState.transform.position, e.transform.position) <= UnitState.AttackRange)
                    .ToList();
                if (targets.Count == 0)
                {
                    State = BattleBehaviorNodeState.Failure;
                    return State;
                }

                UnitState.ResetMana();
                Animator.SetBool(AnimationConstants.IsAbilityBoolean, true);
                UnitState.StartCoroutine(DelayedAbility(targets, GetAnimationTime()));
                State = BattleBehaviorNodeState.Success;
                return State;
            }

            State = BattleBehaviorNodeState.Failure;
            return State;
        }

        private void ThrowBack(UnitStateController enemy, float distance)
        {
            if (distance == 0 || enemy == null)
            {
                return;
            }

            enemy.transform.position += Vector3.back * distance;
        }

        private CircleSwing GetAbilityParams()
        {
            var equip = GetEquip();
            return new CircleSwing(
                equip.doubleEdgePoints switch
                {
                    1 => 10,
                    2 => 25,
                    3 => 45,
                    4 => 60,
                    5 => 80,
                    _ => 0
                },
                equip.midRangePoints switch
                {
                    1 => .5f,
                    2 => 1f,
                    3 => 1.5f,
                    4 => 2f,
                    5 => 3f,
                    _ => 0
                }
            );
        }

        private HumanSpearmanSkills GetEquip()
        {
            return (HumanSpearmanSkills)UnitState.Skills;
        }

        private class CircleSwing
        {
            public readonly long Chance;
            public readonly float Throwback;

            public CircleSwing(long chance, float throwback)
            {
                Chance = chance;
                Throwback = throwback;
            }
        }

        private IEnumerator DelayedAbility(List<UnitStateController> targets, float animationTime)
        {
            yield return new WaitForSeconds(animationTime);
            var abilityParams = GetAbilityParams();
            var rng = new Random();
            foreach (var target in targets)
            {
                var isDoubleDamage = rng.Next(0, 100) > (100 - abilityParams.Chance);
                UnitState.DoAttack(target);
                if (isDoubleDamage)
                {
                    UnitState.DoAttack(target);
                }

                ThrowBack(target, abilityParams.Throwback);
            }
        }
    }
}