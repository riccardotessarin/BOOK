using StateMachine.Actions.Interfaces;
using StateMachine.States;
using UnityEngine;
using Characters.Interfaces;
using Managers;

namespace StateMachine.Actions.Implementations {
    public class InGameMenuInput : IAction{
        private PlayableCharacter player= GameObject.FindWithTag("Player").transform.GetComponent<PlayableCharacter>();
        public void Execute() {
            if (Input.GetKeyUp(KeyCode.Escape)){
                Debug.Log("Esc button pressed!");
            }

            if (Input.GetKeyDown(KeyCode.Q)) {
                GameManager.Instance.CurrentState = new GameState();
                Debug.Log("switching to game state");
                player.UIManager.ActivateMenu(false);
            }
            if(Input.GetKeyDown(KeyCode.F)){
                player.PowerMode = !(player.PowerMode);
                Debug.Log("PowerMode Active: "+player.PowerMode);
                player.UIManager.SwitchMode(player.PowerMode);
            }

            if(Input.GetAxis("Mouse ScrollWheel")!=0){
                var input=Input.GetAxis("Mouse ScrollWheel");
                if(input>0f){
                    player.ScrollUpInventory();
                }
                else if(input<0f){
                    player.ScrollDownInventory();
                }


            }
        }
    }
}