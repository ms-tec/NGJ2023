using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
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
            if (contact.normal == Vector2.up)
            {
                grounded = true;
            }
        }
    }
}
