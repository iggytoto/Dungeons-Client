using System;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(UnitStateController))]
    public class ManaRegenBehavior : MonoBehaviour
    {
        [SerializeField] private float manaRegenInterval = 5;
        private float _timer;
        private UnitStateController _unitState;

        private void Awake()
        {
            _unitState = GetComponent<UnitStateController>();
        }

        private void Update()
        {
            _timer -= Time.deltaTime;
            if (!(_timer <= 0)) return;
            RegenerateMana();
            _timer = manaRegenInterval;
        }

        private void RegenerateMana()
        {
            _unitState.Unit.mana += 10;
        }
    }
}