using Model.Units;

namespace DefaultNamespace.BattleBehaviour.Abilities
{
    public class HumanArcherAbility : UnitTaskBase
    {
        private readonly HumanArcherEquipment _equipment;

        public HumanArcherAbility(UnitStateController unitStateStateController) : base(unitStateStateController)
        {
            _equipment = (HumanArcherEquipment)unitStateStateController.Unit.equip;
        }

        public override BattleBehaviorNodeState Evaluate()
        {
            if (base.Evaluate() == BattleBehaviorNodeState.Failure)
            {
                return BattleBehaviorNodeState.Failure;
            }

            return BattleBehaviorNodeState.Failure;
        }
    }
}