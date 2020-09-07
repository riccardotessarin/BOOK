using StateMachine.Actions.Interfaces;
using StateMachine.States;
using UnityEngine;
using Characters.Interfaces;
using Managers;

namespace StateMachine.Actions.Implementations {
    public class InGameMenuInput : IAction {
        private PlayableCharacter player = GameObject.FindWithTag("Player").transform.GetComponent<PlayableCharacter>();

        public void Execute() {
            if(!player.UIManager.ChatMode){
                if (Input.GetKeyUp(KeyCode.Escape)) {
                    Debug.Log("Esc button pressed!");
                }

                if (Input.GetKeyDown(KeyCode.Q)) {
                    GameManager.Instance.CurrentState = new GameState();
                    Debug.Log("switching to game state");
                    player.UIManager.ActivateMenu(false);
                }

                if (Input.GetKeyDown(KeyCode.F)) {
                    player.PowerMode = !(player.PowerMode);
                    Debug.Log("PowerMode Active: " + player.PowerMode);
                    player.UIManager.SwitchMode(player.PowerMode);
                }

                if (Input.GetAxis("Mouse ScrollWheel") != 0) {
                    var input = Input.GetAxis("Mouse ScrollWheel");
                    if (input > 0f) {
                        player.UIController.ScrollUpInventory();
                    } else if (input < 0f) {
                        player.UIController.ScrollDownInventory();
                    }    
                }

                if (Input.GetMouseButtonDown(0)) {
                    GameManager.Instance.CurrentState = new GameState();
                    Debug.Log("switching to game state");
                    player.UIManager.ActivateMenu(false);
                }
                if(Input.GetKeyDown(KeyCode.T)){
                    Debug.Log("entering chat");
                    player.UIManager.ChangeChatMode();
                }
            }
            else{
                if(Input.GetKeyDown(KeyCode.Escape)){
                    Debug.Log("exitting chat");
                    player.UIManager.ChangeChatMode();
                }
            }
        }
    }
}