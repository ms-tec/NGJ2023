using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentController : MonoBehaviour
{
    public float interval = 2;
    public GameObject[] objectsToSpawn;
    public GameObject player;
    public GameObject speech;
    
    float timer;

    private int counter;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(speech, 7f);
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
            GameObject newSwear = Instantiate(objectsToSpawn[counter], transform.position, transform.rotation);
            //newSwear.GetComponent<SwearController>().player = player;
            timer -= interval;
            
        }
        
        
    }
}
