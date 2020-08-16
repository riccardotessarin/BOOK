using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Actions.Interfaces;
using Characters.Interfaces;
using Manager;

namespace StateMachine.Actions.Implementations{
    public class InGameInputActions : IAction
    {
        public PlayableCharacter Player{get;set;}
        public void Execute(){
            
            if(Input.GetMouseButtonDown(0)){
                Player.Attacker();
                
            }
        }
        
    }
}
