using System;
using UnityEngine;

namespace DefaultNamespace.BattleBehaviour
{
    public abstract class BattleBehaviorTree : MonoBehaviour
    {
        protected BattleBehaviorNode Root;

        private void Start()
        {
            Root = SetupTree();
        }

        protected void Update()
        {
            Root?.Evaluate();
        }

        protected abstract BattleBehaviorNode SetupTree();
    }
}