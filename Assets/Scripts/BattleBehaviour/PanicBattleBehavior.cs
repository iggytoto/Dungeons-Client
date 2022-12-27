using UnityEngine;

namespace DefaultNamespace.BattleBehaviour
{
    [RequireComponent(typeof(UnitStateController))]
    public class PanicBattleBehavior : BattleBehaviorTree
    {
        [SerializeField] public float changeDestinationInterval = 2;

        protected override BattleBehaviorNode SetupTree()
        {
            return new MoveToRandomNearPositionTask(
                gameObject.GetComponent<UnitStateController>(),
                changeDestinationInterval);
        }
    }
}