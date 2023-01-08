using System;
using Model.Units;
using Unity.Netcode;
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
        [SerializeField] private long damage;
        [SerializeField] private UnitType type;
        [SerializeField] private Skills _skills;

        public override UnitType UnitType => type;
        public override Skills Skills => _skills;

        private void Start()
        {
            Unit.Value = new Unit
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
                type = type,
                damage = damage
            };
            _skills = Skills.DefaultForType(type);
        }
    }
}