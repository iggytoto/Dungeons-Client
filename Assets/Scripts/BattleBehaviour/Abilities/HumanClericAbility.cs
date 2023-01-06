using System.Linq;
using DefaultNamespace.BattleBehaviour;
using DefaultNamespace.UnitState;
using Model.Damage;
using Model.Units;

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
                foreach (var target in targets)
                {
                    var isEnemy = target.OwnerId != UnitState.OwnerId;
                    if (isEnemy)
                    {
                        target.DoDamage(Damage.Magic(UnitState.AttackDamage));
                        if (abilityParams.Divine)
                        {
                            target.DoDamage(Damage.Magic((abilityParams.NumberOfTargets - 1) * 5));
                        }
                    }
                    else
                    {
                        target.Heal(UnitState.AttackDamage);
                        if (abilityParams.Divine)
                        {
                            target.Heal((abilityParams.NumberOfTargets - 1) * 5);
                        }
                    }

                    if (abilityParams.Shatter)
                    {
                        var e = target.ApplyEffect<HumanClericShatterEffect>();
                        e.Init(abilityParams.NumberOfTargets, isEnemy ? -25 : 25);
                    }

                    if (abilityParams.Divine)
                    {
                        if (isEnemy)
                        {
                            target.ClearPositiveEffects();
                        }
                        else
                        {
                            target.ClearNegativeEffects();
                        }
                    }
                }
            }

            State = BattleBehaviorNodeState.Failure;
            return State;
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