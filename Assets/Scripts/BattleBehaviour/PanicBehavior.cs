using UnityEngine;

namespace DefaultNamespace.BattleBehaviour
{
    public class PanicBehavior : BaseBattleBehaviour, IBattleBehaviour
    {
        [SerializeField] public float changeDestinationInterval = 2;
        private float _changeDestinationTimer;


        private void Update()
        {
            _changeDestinationTimer -= Time.deltaTime;
            if (!(_changeDestinationTimer <= 0)) return;
            UnitController.MoveTo(GetRandomPositionNearBy());
            _changeDestinationTimer = changeDestinationInterval;
        }

        private Vector3 GetRandomPositionNearBy()
        {
            var random = Random.insideUnitCircle * 5;
            return UnitController.gameObject.transform.position + new Vector3(random.x, 0, random.y);
        }
    }
}