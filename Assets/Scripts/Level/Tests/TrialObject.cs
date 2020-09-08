using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialObject : MonoBehaviour
{
    [SerializeField] protected bool collected=false;
    public bool Collected=>collected;
    public void Collect(){
        collected=true;
        gameObject.SetActive(false);
    }
}
