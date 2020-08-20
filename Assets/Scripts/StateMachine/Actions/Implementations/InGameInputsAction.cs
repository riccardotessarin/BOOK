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
                if(player.PowerMode)
                    player.Attacker();
                else{
                    player.UseEquippedConsumable();
                }
                
            }
            if (Input.GetKeyDown(KeyCode.Q)) {
                GameManager.Instance.CurrentState = new PauseMenuState();
                Debug.Log("Changing to pause menu state");
                player.UIManager.ActivateMenu(true);

            }
            if(Input.GetKeyDown(KeyCode.F)){
                player.PowerMode = !(player.PowerMode);
                Debug.Log("PowerMode Active: "+player.PowerMode);
                player.UIManager.SwitchMode(player.PowerMode);
            }
        }
        
    }
}
