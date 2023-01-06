using System.Linq;
using DefaultNamespace.BattleBehaviour;
using Model.Damage;
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

                UnitState.Mana = 0;
                var abilityParams = GetAbilityParams();
                var rng = new Random();
                foreach (var target in targets)
                {
                    var isDoubleDamage = rng.Next(0, 100) > (100 - abilityParams.Chance);
                    target.DoDamage(Damage.Physical(UnitState.AttackDamage));
                    if (isDoubleDamage)
                    {
                        target.DoDamage(Damage.Physical(UnitState.AttackDamage));
                    }

                    ThrowBack(target, abilityParams.Throwback);
                }
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

        private HumanSpearmanEquipment GetEquip()
        {
            return (HumanSpearmanEquipment)UnitState.Equipment;
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
    }
}