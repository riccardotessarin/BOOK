using StateMachine.Actions.Interfaces;
using StateMachine.States;
using UnityEngine;
using Characters.Interfaces;

namespace StateMachine.Actions.Implementations {
    public class InGameMenuInput : IAction{
        public PlayableCharacter Player{get;set;}
        public void Execute() {
            if (Input.GetKeyUp(KeyCode.Escape)){
                Debug.Log("Esc button pressed!");
            }

            if (Input.GetKeyUp(KeyCode.Q)) {
                GameManager.Instance.CurrentState = new GameState();
                Debug.Log("switching to game state");
            }
        }
    }
}