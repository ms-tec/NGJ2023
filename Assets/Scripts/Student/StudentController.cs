using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentController : MonoBehaviour
{
    public float interval = 2;
    public GameObject[] objectsToSpawn;
    public GameObject player;
    public GameObject speech;
    [SerializeField, Range(1, 10)] private float speechTime;
    
    float timer;

    private int counter;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(speech, speechTime);
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
            counter = (counter + 1) % objectsToSpawn.Length;
            Instantiate(objectsToSpawn[counter], transform.position, transform.rotation);
            //newSwear.GetComponent<SwearController>().player = player;
            timer -= interval;
            
        }
        
        
    }
}
