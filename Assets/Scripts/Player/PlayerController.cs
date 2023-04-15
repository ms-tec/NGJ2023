using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CheckCollision))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField, Range(0, 1000)] private float moveSpeed;
    [SerializeField, Range(0, 5)] private float upwardMobility;
    [SerializeField, Range(0, 5)] private float downwardMobility;
    [SerializeField, Range(0, 10)] private float jumpHeight;
    [SerializeField, Range(0, 30)] private float airVelocityChange;
    [SerializeField, Range(0, 1000)] private float velocityChange;

    private PlayerInput input;
    private Rigidbody2D rb;
    private CheckCollision colCheck;

    // Movement input
    private float _moveInput = 0f;
    private bool _jumpDesired = false;

    // Movement calculation
    private float _jumpSpeed;
    private Vector2 _velocity;
    private bool _isGrounded;
    private bool _isJumping = false;
    private float _velocityChange = 0f;

    private Animator animator;
    private SpriteRenderer sRenderer; 

    void Start()
    {
        colCheck = GetComponent<CheckCollision>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>(); 
        input = new PlayerInput();
        input.Enable();
        input.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<float>();
        input.Player.Move.canceled += _ => _moveInput = 0;
        input.Player.Jump.started += _ => _jumpDesired = true;
    }

    private void Update() {
        animator.SetBool("Jumping", _isJumping);
        animator.SetFloat("moveSpeed", Mathf.Abs(_moveInput));

        if (_moveInput < 0)
        {
            sRenderer.flipX = true;
        }
        else if (_moveInput > 0)
        {
            sRenderer.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        _isGrounded = colCheck.IsGrounded();
        _velocity = rb.velocity;

        if(_isGrounded)
        {
            _velocityChange = velocityChange;
        } else
        {
            _velocityChange = airVelocityChange;
        }

        Move();
        Jump();
        

        rb.velocity = _velocity;
    }

    private void Move()
    {
        _velocity.x = Mathf.MoveTowards(_velocity.x, _moveInput * moveSpeed * Time.fixedDeltaTime, _velocityChange * Time.fixedDeltaTime);
        //_velocity = new Vector2(_moveInput * moveSpeed * Time.fixedDeltaTime, _velocity.y);
    }

    private void Jump()
    {

        // How fast do we accelerate down?
        if(_velocity.y > 0 && !_isGrounded)
        {
            rb.gravityScale = upwardMobility;
            
        } else if(_velocity.y < 0)
        {
            rb.gravityScale = downwardMobility;
        } else
        {
            rb.gravityScale = 1;
        }

        // Are we landing?
        if(_isJumping && _isGrounded && _velocity.y <= 0)
        {
            _isJumping = false;
        }

        // Are we initializing a jump?
        if (_jumpDesired && _isGrounded)
        {
            _isJumping = true;
            _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight * upwardMobility);

            _velocity.y = _jumpSpeed;
        }
        _jumpDesired = false;
    }

    private void OnDrawGizmos()
    {
        if(_isJumping)
        {
            Gizmos.color = Color.green;
        } else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireSphere(transform.position + 0.5f * Vector3.up, 0.5f);
    }
}
