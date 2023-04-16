using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CheckCollision))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField, Range(0, 1000)] private float moveSpeed;
    [SerializeField, Range(0, 1000)] private float velocityChange;

    [Header("Jumping")]
    [SerializeField, Range(0, 10)] private float jumpHeight;
    [SerializeField, Range(0, 5)] private float upwardMobility;
    [SerializeField, Range(0, 5)] private float downwardMobility;
    [SerializeField, Range(0, 30)] private float airVelocityChange;
    [SerializeField, Range(0, 1)] private float getOffGroundTime = 0.1f;
    [SerializeField, Range(0, 2)] private float coyoteTime;

    [Header("Jetpack")]
    [SerializeField, Range(0, 1000)] private float jetpackSpeed;

    private PlayerInput input;
    private Rigidbody2D rb;
    private CheckCollision colCheck;

    // Jetpack
    [HideInInspector] public bool _hasJetpack = false;
    private bool _isFlying = false;
    private bool _jetpackActive = false;

    // Movement input
    private float _moveInput = 0f;
    private bool _jumpDesired = false;
    private bool _jetpackDesired = false;

    // Movement calculation
    private float _jumpSpeed;
    private Vector2 _velocity;
    private bool _isGrounded;
    private bool _isJumping = false;
    private float _velocityChange = 0f;

    // Jump timers
    private float _getOffGroundTime = 0f;
    private float _coyoteTime = 0f;

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
        input.Player.Jetpack.performed += _ => _jetpackDesired = true;
        input.Player.Jetpack.canceled += _ => _jetpackDesired = false;
    }

    private void Update() {
        animator.SetBool("Jumping", _isJumping);
        animator.SetFloat("moveSpeed", Mathf.Abs(_moveInput));
        animator.SetBool("Flying", _isFlying);
        animator.SetBool("JetpackActive", _isFlying && _jetpackActive);

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

        if(_isGrounded || _jetpackActive)
        {
            _velocityChange = velocityChange;
            _coyoteTime = coyoteTime;

        } else
        {
            _velocityChange = airVelocityChange;
            _coyoteTime -= Time.deltaTime;
        }

        Move();
        Jump();
        Jetpack();
        

        rb.velocity = _velocity;
    }

    private void Move()
    {
        _velocity.x = Mathf.MoveTowards(_velocity.x, _moveInput * moveSpeed * Time.fixedDeltaTime, _velocityChange * Time.fixedDeltaTime);
    }

    private void Jump()
    {

        // How fast do we accelerate down?
        if(_isGrounded)
        {
            rb.gravityScale = 0;
        } else if(_velocity.y > 0 && !_isGrounded)
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
        if(_isJumping && _isGrounded && _getOffGroundTime <= 0)
        {
            _isJumping = false;
        } else if(_isJumping)
        {
            _getOffGroundTime -= Time.fixedDeltaTime;
        }

        // Are we initializing a jump?
        if (_jumpDesired && (_isGrounded || _coyoteTime > 0) && !_isJumping)
        {
            _isJumping = true;
            _getOffGroundTime = getOffGroundTime;
            _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight * upwardMobility);

            _velocity.y = _jumpSpeed;
        } else if(!_isJumping && _isGrounded)
        {
            //_velocity.y = 0;
        }
        _jumpDesired = false;
    }

    private void Jetpack()
    {
        Debug.Log("Jetpack: " + _jetpackDesired);
        if (_isFlying && _isGrounded && _getOffGroundTime <= 0)
        {
            _isFlying = false;
        } else if(_isFlying)
        {
            _getOffGroundTime -= Time.fixedDeltaTime;
        }

        if (_jetpackDesired && _hasJetpack)
        {
            _isFlying = true;
            _jetpackActive = true;
            _isJumping = false;
            _velocity.y = jetpackSpeed * Time.fixedDeltaTime;
            if(_isGrounded)
            {
                _getOffGroundTime = getOffGroundTime;
            }
        } else
        {
            _jetpackActive = false;
        }
    }

    private void OnDrawGizmos()
    {
        if(_isGrounded)
        {
            Gizmos.color = Color.green;
        } else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireSphere(transform.position + 0.5f * Vector3.up, 0.5f);
    }
}
