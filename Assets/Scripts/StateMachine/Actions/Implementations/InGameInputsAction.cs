using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Actions.Interfaces;
using Characters.Interfaces;
using Managers;
using StateMachine.States;

namespace StateMachine.Actions.Implementations {
    public class InGameInputsAction : IAction {
        private readonly PlayableCharacter _player = GameObject.FindWithTag("Player").transform.GetComponent<PlayableCharacter>();

        public void Execute() {
            
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            if (!_player.IsAttacking || _player.IsDeath) {
                HandlePlayerInputs();
            }
        }

        private void HandlePlayerInputs() {
            if(!_player.UIManager.ChatMode){
                if (Input.GetMouseButtonDown(0)) {
                    if (_player.PowerMode)
                        _player.Attacker();
                    else {
                        _player.UseEquippedConsumable();
                    }
                }

                if (Input.GetKeyDown(KeyCode.Q)) {
                    GameManager.Instance.CurrentState = new PauseMenuState();
                    Debug.Log("Changing to pause menu state");
                    _player.UIManager.App_disViewfinder(false);
                    _player.UIManager.ActivateMenu(true);
                }

                if (Input.GetKeyDown(KeyCode.F)) {
                    _player.PowerMode = !(_player.PowerMode);
                    Debug.Log("PowerMode Active: " + _player.PowerMode);
                    _player.UIManager.SwitchMode(_player.PowerMode);
                }

                if (Input.GetKeyDown(KeyCode.X)) {
                    Debug.Log("Trying to loot");
                    _player.LootAction();
                }

                if (Input.GetKeyDown(KeyCode.E)) {
                    Debug.Log("Trying to interact");
                    _player.InteractAction();
                }
                if(Input.GetKeyDown(KeyCode.T)){
                    Debug.Log("entering chat");
                    _player.UIManager.ChangeChatMode();
                }
                if(Input.GetKeyDown(KeyCode.Escape)){
                    Application.Quit();
                }
            }
            else{
                if(Input.GetKeyDown(KeyCode.Escape)){
                    Debug.Log("exitting chat");
                    _player.UIManager.ChangeChatMode();
                }
            }
        }
    }
}