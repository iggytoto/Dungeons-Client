using UnityEngine;

namespace DefaultNamespace
{
    public class DebugUnitStateController : UnitStateController
    {
        [SerializeField] private long id;
        [SerializeField] private long ownerId;
        [SerializeField] private long hp;
        [SerializeField] private long maxHp;
        [SerializeField] private float attackRange;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float attackSpeed;
        
        public override Unit Unit => new()
        {
            Id = id,
            ownerId = ownerId,
            hitPoints = hp,
            maxHp = maxHp,
            attackRange = attackRange,
            movementSpeed = moveSpeed,
            attackSpeed = attackSpeed
        };
    }
}