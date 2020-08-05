using UnityEngine;
using System.Collections;
using StateMachine.Actions.Interfaces;
using StateMachine.States;

namespace StateMachine.Actions.Implementations {
	public class InGameInputsAction : IAction {
		public void Execute() {
			if (Input.GetKeyUp(KeyCode.Q)) {
				GameManager.Instance.CurrentState = new PauseMenuState();
				Debug.Log("Changing to pause menu state");
			}
		}

	}
}