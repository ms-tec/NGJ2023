using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    [SerializeField, Range(0, 90)] private float maxSlopeAngle = 30;

    private bool grounded = false;

    public bool IsGrounded()
    {
        return grounded;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EvaluateCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EvaluateCollision(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
    }

    void EvaluateCollision(Collision2D collision)
    {
        for(int i = 0; i < collision.contactCount; i++)
        {
            ContactPoint2D contact = collision.GetContact(i);
            float angle = Vector2.SignedAngle(contact.normal, Vector2.up);
            if (Mathf.Abs(angle) < maxSlopeAngle)
            {
                grounded = true;
            }
        }
    }
}
