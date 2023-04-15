using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float offset;
    public float offsetSmoothing;
    public float offsetSensitivity = 0.5f;

    private Vector3 playerPosition;
    private Vector3 lastPosition;
    private Vector3 targetPosition;
    private float lastMoveDir;

    // Update is called once per frame 
    // Sammenlign camerapos  
    void Update()
    {   

        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        targetPosition = playerPosition;
        if (playerPosition.x > lastPosition.x + offsetSensitivity){
            lastMoveDir = 1;
            lastPosition = playerPosition;
        }
        else if(playerPosition.x < lastPosition.x - offsetSensitivity) {
            lastMoveDir = -1;
            lastPosition = playerPosition;
        }
        targetPosition.x += offset * lastMoveDir;
        Debug.Log("Player: " + playerPosition);
        Debug.Log("Target: " + targetPosition);
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, offsetSmoothing * Time.deltaTime);
        
    }
}
