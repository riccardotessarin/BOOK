using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Actions.Interfaces;
using Characters.Interfaces;
using Manager;

namespace StateMachine.Actions.Implementations{
    public class InGameInput : IAction
    {
        public void Execute(){
            if(Input.GetMouseButtonDown(0)){
                Debug.Log("left mouse button pressed");
                
            }
        }
        
    }
}
