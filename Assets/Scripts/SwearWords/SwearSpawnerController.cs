using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwearSpawnerController : MonoBehaviour
{
    public float interval = 5;
    public GameObject[] objectsToSpawn;
    
    float timer;

    private int counter;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    /*
    Max levetid -> Objektet selv
    Bevægelse -> Objektet selv
    Spiller fuel -> Spiller-objektet
    Kaste hen mod spilleren (måske som en parabelbue)
    */
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= interval)
        {
            if(counter < 4) counter += 1; else counter = 0;
            Instantiate(objectsToSpawn[counter], transform.position, transform.rotation);
            timer -= interval;
            
        }
        
        
    }
}
