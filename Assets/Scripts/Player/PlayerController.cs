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

    private void update(){
        animator.SetBool("Grounded", colCheck);
    }

    private void FixedUpdate()
    {
        _velocity = rb.velocity;

        Move();
        Jump();
        Animate();
        

        rb.velocity = _velocity;
    }

    private void Animate()
    {
        Debug.Log(Mathf.Abs(_moveInput));
        if (Mathf.Abs(_moveInput) < 0.1 && Mathf.Abs(_moveInput) > -0.1 ) {
            animator.Play("Idle");
            
        }
        else if (_moveInput < 0){
            
            //transform.scale = new Vector3(-1,1,1)
            sRenderer.flipX = true;
            animator.speed = 0.1F;
            animator.Play("Walk");
        }
        else if (_moveInput > 0){
            
            sRenderer.flipX = false;
            animator.speed = 0.1F;
            animator.Play("Walk");
        }
       

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
        } else
        {
            rb.gravityScale = 1;
        }

        // Are we initializing a jump?
        if (_jumpDesired && colCheck.IsGrounded())
        {
            
            _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight * upwardMobility);

            _velocity.y += _jumpSpeed;
            animator.SetTrigger("Jump");
        }
        _jumpDesired = false;
    }
}
