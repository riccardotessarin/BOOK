using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Actions.Interfaces;
using Characters.Interfaces;
using Managers;
using StateMachine.States;

namespace StateMachine.Actions.Implementations{
    public class InGameInputsAction : IAction
    {
        private PlayableCharacter player= GameObject.FindWithTag("Player").transform.GetComponent<PlayableCharacter>();
        public void Execute(){
            
            if(Input.GetMouseButtonDown(0)){
                player.Attacker();
                
            }
            if (Input.GetKeyUp(KeyCode.Q)) {
                GameManager.Instance.CurrentState = new PauseMenuState();
                Debug.Log("Changing to pause menu state");
            }
        }
        
    }
}
