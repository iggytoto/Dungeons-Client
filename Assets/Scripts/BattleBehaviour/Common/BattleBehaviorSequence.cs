using System.Collections.Generic;

namespace DefaultNamespace.BattleBehaviour
{
    public class BattleBehaviorSequence : BattleBehaviorNode
    {
        public BattleBehaviorSequence() : base()
        {
        }

        public BattleBehaviorSequence(List<BattleBehaviorNode> children) : base(children)
        {
        }

        public override BattleBehaviorNodeState Evaluate()
        {
            var anyChildRunning = false;
            foreach (var child in Children)
            {
                switch (child.Evaluate())
                {
                    case BattleBehaviorNodeState.Running:
                        anyChildRunning = true;
                        continue;
                    case BattleBehaviorNodeState.Success:
                        continue;
                    case BattleBehaviorNodeState.Failure:
                        State = BattleBehaviorNodeState.Failure;
                        return State;
                    default:
                        State = BattleBehaviorNodeState.Success;
                        return State;
                }
            }

            State = anyChildRunning ? BattleBehaviorNodeState.Running : BattleBehaviorNodeState.Success;
            return State;
        }
    }
}