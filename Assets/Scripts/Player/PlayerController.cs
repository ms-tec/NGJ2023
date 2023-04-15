using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CheckCollision))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float upwardMobility;
    [SerializeField] private float downwardMobility;
    [SerializeField] private float jumpHeight;

    private PlayerInput input;
    private Rigidbody2D rb;
    private CheckCollision colCheck;

    // Movement input
    private float _moveInput = 0f;
    private bool _jumpDesired = false;

    // Movement calculation
    private float _jumpSpeed;
    private Vector2 _velocity;

    void Start()
    {
        colCheck = GetComponent<CheckCollision>();
        rb = GetComponent<Rigidbody2D>();
        input = new PlayerInput();
        input.Enable();
        input.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<float>();
        input.Player.Move.canceled += _ => _moveInput = 0;
        input.Player.Jump.started += _ => _jumpDesired = true;
    }

    private void FixedUpdate()
    {
        _velocity = rb.velocity;

        Move();
        Jump();

        rb.velocity = _velocity;
    }

    private void Move()
    {
        _velocity = new Vector2(_moveInput * moveSpeed * Time.fixedDeltaTime, _velocity.y);
    }

    private void Jump()
    {
        // How fast do we accelerate down?
        if(_velocity.y > 0)
        {
            rb.gravityScale = upwardMobility;
        } else if(_velocity.y < 0)
        {
            rb.gravityScale = downwardMobility;
        }

        // Are we initializing a jump?
        if(_jumpDesired && colCheck.IsGrounded())
        {
            _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight * upwardMobility);

            _velocity.y += _jumpSpeed;
        }
        _jumpDesired = false;
    }
}
