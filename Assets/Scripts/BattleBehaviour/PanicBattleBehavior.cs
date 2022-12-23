using UnityEngine;

namespace DefaultNamespace.BattleBehaviour
{
    public class PanicBattleBehavior : NavMeshAgentBasedBattleBehaviour, IBattleBehaviour
    {
        [SerializeField] public float changeDestinationInterval = 2;
        private float _changeDestinationTimer;


        private new void Update()
        {
            if(UnitController.Unit.IsDead()) return;
            _changeDestinationTimer -= Time.deltaTime;
            if (_changeDestinationTimer <= 0)
            {
                SetPathTo(GetRandomPositionNearBy());
                _changeDestinationTimer = changeDestinationInterval;
            }

            base.Update();
        }

        private Vector3 GetRandomPositionNearBy()
        {
            var random = Random.insideUnitCircle * 5;
            return UnitController.gameObject.transform.position + new Vector3(random.x, 0, random.y);
        }
    }
}