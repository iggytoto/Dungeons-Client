using UnityEngine;

namespace DefaultNamespace.BattleBehaviour
{
    [RequireComponent(typeof(UnitController))]
    public class BaseBattleBehaviour : MonoBehaviour
    {
        protected UnitController UnitController;

        private void Start()
        {
            UnitController = gameObject.GetComponent<UnitController>();
        }
    }
}