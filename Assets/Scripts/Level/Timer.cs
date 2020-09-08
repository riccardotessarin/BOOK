using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeRemaining;
    public bool timeIsRunning=false;
    public bool endReached=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeIsRunning){
            if(timeRemaining>0)
                timeRemaining-=Time.deltaTime;
            else
                endReached=true;
        }
    }
}
