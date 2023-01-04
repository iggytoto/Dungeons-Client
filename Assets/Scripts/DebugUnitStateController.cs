using System;
using Model.Units;
using UnityEngine;

namespace DefaultNamespace
{
    public class DebugUnitStateController : UnitStateController
    {
        [SerializeField] private long id;
        [SerializeField] private long ownerId;
        [SerializeField] private long hp;
        [SerializeField] private long maxHp;
        [SerializeField] private long maxMana;
        [SerializeField] private long mana;
        [SerializeField] private float attackRange;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float attackSpeed;
        [SerializeField] private UnitType type;

        private Unit _unit = new Unit();

        private void Awake()
        {
            _unit = new Unit
            {
                Id = id,
                ownerId = ownerId,
                hitPoints = hp,
                maxHp = maxHp,
                attackRange = attackRange,
                movementSpeed = moveSpeed,
                attackSpeed = attackSpeed,
                mana = mana,
                maxMana = maxMana,
                type = type
            };
        }

        public override Unit Unit => _unit;
    }
}