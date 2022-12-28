using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace.BattleBehaviour
{
    public abstract class NavMeshAgentUnitTaskBase : UnitTaskBase
    {
        protected readonly NavMeshAgent NavMeshAgent;
        protected readonly List<Vector3> Path = new();

        protected NavMeshAgentUnitTaskBase(UnitStateController unitStateController, NavMeshAgent navMeshAgent) : base(
            unitStateController)
        {
            NavMeshAgent = navMeshAgent;
        }

        protected void RecalculatePath(Vector3 destination)
        {
            Path.Clear();

            var nmp = new NavMeshPath();
            NavMeshAgent.CalculatePath(destination, nmp);

            Path.AddRange(nmp.corners);
            if (Path.Any() && Vector3.Distance(Unit.transform.position, Path.First()) <= .01f)
            {
                Path.RemoveAt(0);
            }
        }

        protected void Move()
        {
            if (!Path.Any()) return;
            Unit.transform.position = Vector3.MoveTowards(
                Unit.transform.position,
                Path[0],
                Unit.GetCurrentSpeed() * Time.deltaTime);
            Unit.transform.LookAt(Path[0]);
        }
    }
}