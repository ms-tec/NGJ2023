using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAttack : MonoBehaviour
{
    public Transform positionUp;
    public Transform positionDown;
    public float moveSpeed = 5;

    private Vector3 position;
    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = positionUp.position;
    }

    private void Update()
    {
        position = transform.position;

        if(position == positionDown.position)
        {
            targetPosition = positionUp.position;
        }

        position = Vector3.MoveTowards(position, targetPosition, Time.deltaTime * moveSpeed);

        transform.position = position;
    }

    public void Attack()
    {
        if (position == positionUp.position)
        {
            targetPosition = positionDown.position;
        }
    }
}
