using System.Collections.Generic;

namespace DefaultNamespace.BattleBehaviour
{
    public class BattleBehaviorSelector : BattleBehaviorNode
    {
        public BattleBehaviorSelector() : base()
        {
        }

        public BattleBehaviorSelector(List<BattleBehaviorNode> children) : base(children)
        {
        }

        public override BattleBehaviorNodeState Evaluate()
        {
            foreach (var child in Children)
            {
                switch (child.Evaluate())
                {
                    case BattleBehaviorNodeState.Running:
                        State = BattleBehaviorNodeState.Running;
                        return State;
                    case BattleBehaviorNodeState.Success:
                        State = BattleBehaviorNodeState.Success;
                        return State;
                    case BattleBehaviorNodeState.Failure:
                        continue;
                    default:
                        continue;
                }
            }

            State = BattleBehaviorNodeState.Failure;
            return State;
        }
    }
}