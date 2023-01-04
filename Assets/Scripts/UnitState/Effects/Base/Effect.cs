using System;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace.UnitState
{
    /**
     * Base class for effect for a unit.
     * Effect instantiates as unit component and affects it on a fixed amount of time, automatically destroys itself after
     * time is passed.
     */
    [RequireComponent(typeof(UnitStateController))]
    public abstract class Effect : MonoBehaviour
    {
        private float _currentDuration;
        protected UnitStateController UnitState;
        protected abstract float EffectDuration { get; }
        public abstract long Id { get; }
        public event Action OnDestroy;

        private void Awake()
        {
            UnitState = gameObject.GetComponent<UnitStateController>();
        }

        protected virtual void Update()
        {
            _currentDuration += Time.deltaTime;
            if (!(_currentDuration >= EffectDuration)) return;
            OnDestroy?.Invoke();
            Destroy(this);
        }
    }
}