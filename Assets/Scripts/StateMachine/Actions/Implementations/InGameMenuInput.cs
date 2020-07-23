using StateMachine.Actions.Interfaces;
using UnityEngine;

namespace StateMachine.Actions.Implementations {
    public class InGameMenuInput : IAction{
        public void Execute() {
            if (Input.GetKeyUp(KeyCode.Escape)){
                Debug.Log("Esc button pressed!");
            }
        }
    }
}