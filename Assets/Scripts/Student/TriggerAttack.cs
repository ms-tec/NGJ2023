using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerAttack : MonoBehaviour
{
    public UnityEvent attack;
    private BoxCollider2D col;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            attack.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        col = GetComponent<BoxCollider2D>();
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + new Vector3(col.offset.x, col.offset.y, 0), col.size);
    }
}
