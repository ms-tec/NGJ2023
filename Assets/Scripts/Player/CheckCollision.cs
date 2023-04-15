using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    [SerializeField, Range(0, 90)] private float maxSlopeAngle = 30;

    private bool grounded = false;
    private ContactPoint2D[] contacts = new ContactPoint2D[10];

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
        collision.GetContacts(contacts);
        foreach (ContactPoint2D contact in contacts)
        {
            float angle = Vector2.Angle(contact.normal, Vector2.up);
            if (angle < maxSlopeAngle)
            {
                grounded = true;
            }
        }
    }
}
