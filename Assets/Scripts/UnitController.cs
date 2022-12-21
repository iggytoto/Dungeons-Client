using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Animations;
using DefaultNamespace.BattleBehaviour;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitController : NetworkBehaviour
{
    public Unit Unit => _unit.Value;

    private readonly NetworkVariable<Unit> _unit = new();
    private Animator _animator;
    private bool _isMoving;
    private NavMeshAgent _navMeshAgent;
    private readonly List<Vector3> _movementPath = new();

    public void Init(Unit unit)
    {
        _unit.Value = unit;
        BattleBehaviourManager.UpdateBattleBehaviour(this);
    }

    public Unit ToUnit()
    {
        return _unit.Value;
    }

    public bool IsDead()
    {
        return _unit.Value.HitPoints <= 0;
    }

    public void MoveTo(Vector3 destination)
    {
        Stop();
        _isMoving = true;
        var nmp = new NavMeshPath();
        _navMeshAgent.CalculatePath(destination, nmp);
        _movementPath.AddRange(nmp.corners);
        _animator.SetBool(CommonAnimationsConstants.IsAttacking, false);
        _animator.SetBool(CommonAnimationsConstants.IsRunning, true);
    }

    private void Start()
    {
        _animator = gameObject.GetComponentInChildren<Animator>();
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_isMoving)
        {
            ProcessMovement();
        }
    }

    private void ProcessMovement()
    {
        if (!_movementPath.Any()) return;
        var currentPosition = gameObject.transform.position;
        if (Vector3.Distance(currentPosition, _movementPath[0]) <= .01)
        {
            _movementPath.RemoveAt(0);
        }

        if (!_movementPath.Any())
        {
            return;
        }

        var nextPointToMove = _movementPath[0];
        transform.position =
            Vector3.MoveTowards(transform.position, nextPointToMove, _unit.Value.MovementSpeed * Time.deltaTime);
        transform.LookAt(nextPointToMove);
        if (Vector3.Distance(transform.position, nextPointToMove) < 0.01f)
        {
            Stop();
        }
    }

    private void Stop()
    {
        _animator.SetBool(CommonAnimationsConstants.IsRunning, false);
        _animator.SetBool(CommonAnimationsConstants.IsAttacking, false);
        _movementPath.Clear();
    }
}