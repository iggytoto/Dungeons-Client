using UnityEngine;

namespace DefaultNamespace.BattleBehaviour
{
    [RequireComponent(typeof(UnitController))]
    public abstract class BaseBattleBehaviour : MonoBehaviour
    {
        protected UnitController UnitController;
        protected float _attackCooldown;
        protected UnitController Target;

        protected void Start()
        {
            UnitController = gameObject.GetComponent<UnitController>();
        }

        protected void Update()
        {
            if(UnitController.Unit.IsDead()) return;
            _attackCooldown -= Time.deltaTime;
        }

        protected bool CanAttack()
        {
            return Target != null &&
                   _attackCooldown <= 0 &&
                   Vector3.Distance(Target.transform.position, UnitController.transform.position) <=
                   UnitController.Unit.AttackRange;
        }
    }
}