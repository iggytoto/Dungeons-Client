using DefaultNamespace.BattleBehaviour;
using Unity.Netcode;
using UnityEngine;


public class UnitController : NetworkBehaviour
{
    public Unit Unit => _unit.Value;

    private readonly NetworkVariable<Unit> _unit = new();
    private Animator _animator;
    private const float AttackAnimationTime = 2.267f;

    public void Init(Unit unit)
    {
        _unit.Value = unit;
        unit.OnDeath += OnUnitDeath;
        BattleBehaviourManager.UpdateBattleBehaviour(this);
    }

    private void OnUnitDeath()
    {
        _animator.SetTrigger("Death");
    }

    public Unit ToUnit()
    {
        return _unit.Value;
    }

    private void Start()
    {
        _animator = gameObject.GetComponentInChildren<Animator>();
    }

    public void Attack(Vector3 destination, float animationTime)
    {
        Stop();
        transform.LookAt(destination);
        _animator.SetTrigger("Attack");
        _animator.speed = AttackAnimationTime / animationTime;
    }

    public void Move(Vector3 destination)
    {
        Stop();
        _animator.SetBool("IsRunning", true);
        transform.position =
            Vector3.MoveTowards(transform.position, destination, _unit.Value.MovementSpeed * Time.deltaTime);
        transform.LookAt(destination);
        if (Vector3.Distance(transform.position, destination) < 0.01f)
        {
            Stop();
        }
    }

    public void Stop()
    {
        _animator.speed = 1;
        _animator.SetBool("IsRunning", false);
    }
}