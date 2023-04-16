using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwearController : MonoBehaviour
{
    public GameObject player;
    private Vector3 playerPos;
    private float ranNum; 
    public float speed = 30.0f;
    public float rotate = 0.1f;
    // Start is called before the first frame update
    void Start()
    {   
        
        player = GameObject.FindWithTag("Player");
        ranNum = Random.Range(-10.0f, 10.0f);
        playerPos = new Vector3(player.transform.position.x + ranNum, player.transform.position.y, player.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, playerPos, step);
        if (transform.position == playerPos) Destroy(gameObject);
        transform.Rotate(0,0,rotate);

        
            
        
    }
}
