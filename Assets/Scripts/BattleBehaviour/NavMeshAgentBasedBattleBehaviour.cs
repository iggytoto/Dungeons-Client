using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace.BattleBehaviour
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class NavMeshAgentBasedBattleBehaviour : BaseBattleBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private readonly List<Vector3> _movementPath = new();

        protected new void Start()
        {
            base.Start();
            _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        }

        protected new void Update()
        {
            if(UnitController.Unit.IsDead()) return;
            base.Update();
            var currentPosition = gameObject.transform.position;

            if (!_movementPath.Any())
            {
                return;
            }

            UnitController.Move(_movementPath[0]);
            if (Vector3.Distance(currentPosition, _movementPath[0]) <= .01)
            {
                _movementPath.RemoveAt(0);
            }
        }

        protected void SetPathTo(Vector3 destination)
        {
            var nmp = new NavMeshPath();
            _navMeshAgent.CalculatePath(destination, nmp);
            _movementPath.Clear();
            _movementPath.AddRange(nmp.corners);
        }
    }
}