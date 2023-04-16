using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwearController : MonoBehaviour
{
    public GameObject player;
    private Vector3 playerPos;
    public float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, playerPos, step);
    }
}
