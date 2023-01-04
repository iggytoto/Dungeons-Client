using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace.BattleBehaviour
{
    public abstract class NavMeshAgentUnitTaskBase : UnitTaskBase
    {
        private readonly NavMeshAgent _navMeshAgent;
        protected readonly List<Vector3> Path = new();

        protected NavMeshAgentUnitTaskBase(UnitStateController unitStateStateController, NavMeshAgent navMeshAgent) : base(
            unitStateStateController)
        {
            _navMeshAgent = navMeshAgent;
        }

        protected void RecalculatePath(Vector3 destination)
        {
            Path.Clear();

            var nmp = new NavMeshPath();
            _navMeshAgent.CalculatePath(destination, nmp);

            Path.AddRange(nmp.corners);
            if (Path.Any() && Vector3.Distance(UnitState.transform.position, Path.First()) <= .01f)
            {
                Path.RemoveAt(0);
            }
        }

        protected void Move()
        {
            if (!Path.Any()) return;
            UnitState.transform.position = Vector3.MoveTowards(
                UnitState.transform.position,
                Path[0],
                UnitState.GetCurrentSpeed() * Time.deltaTime);
            UnitState.transform.LookAt(Path[0]);
        }
    }
}