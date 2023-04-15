using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    private PlayerInput input;
    private Rigidbody2D rb;

    private float currentMoveSpeed = 0f;

    void Start()
    {
        input = new PlayerInput();
        input.Enable();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        currentMoveSpeed = input.Player.Move.ReadValue<float>();
        Debug.Log(currentMoveSpeed);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(currentMoveSpeed * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }
}
