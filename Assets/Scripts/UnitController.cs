using System;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _animator;
    private Vector3 _unitVelocity;
    private bool _grounded;
    private bool _isMoving;
    private bool _isAttacking;
    private float _speed;


    void Start()
    {
        _characterController = gameObject.AddComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _grounded = _characterController.isGrounded;
        if (_grounded && _unitVelocity.y < 0)
        {
            _unitVelocity.y = 0f;
        }

        Move();
        Animate();
    }

    private void Animate()
    {
        
    }

    private void Move()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _characterController.Move(move * Time.deltaTime * _speed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }

    public void Init(Unit unit)
    {
        
    }

    public Unit ToUnit()
    {
        throw new NotImplementedException();
    }

    public bool IsDead()
    {
        return false;
    }
}