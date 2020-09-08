using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialButton : MonoBehaviour
{
    public bool pressed;
    // Start is called before the first frame update
    void Start()
    {
        pressed=false;
    }

    // Update is called once per frame
    
    public void PressButton(){
        pressed=true;
        GetComponent<Collider>().enabled=false;
    }
}
