using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float offset;
    public float offsetSmoothing;
    private Vector3 playerPosition;
    private Vector3 lastPosition;
    private Vector3 targetPosition;
    private float lastMoveDir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame 
    // Sammenlign camerapos  
    void Update()
    {   

        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z); 
        targetPosition = playerPosition;  
        if (playerPosition.x > lastPosition.x){
            lastMoveDir = 1;
        }
        else if(playerPosition.x < lastPosition.x) {
            lastMoveDir = -1;
        }
        targetPosition.x += offset * lastMoveDir;
        Debug.Log("Player: " + playerPosition);
        Debug.Log("Target: " + targetPosition);
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, offsetSmoothing * Time.deltaTime);
        
        lastPosition = playerPosition;
    }
}