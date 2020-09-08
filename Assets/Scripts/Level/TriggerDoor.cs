using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Interfaces;

public class TriggerDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider){
        if(collider.GetComponent<PlayableCharacter>()){
            WinFunction();
        }
    }
    private void WinFunction(){
        Debug.Log("You win");
    }
}
