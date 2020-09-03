using StateMachine.States;
using UnityEngine;

namespace Managers {
	public class GameManager : MonoBehaviour {
		public static GameManager Instance { get; private set; }
		public State CurrentState { get; set; }

		private void Awake() {
			if (Instance == null) {
				Instance = this;
			} else {
				Destroy(this);
			}
		}

		// Start is called before the first frame update
		private void Start() {
			CurrentState = new InGameState();
		}

		// Update is called once per frame
		private void Update() {
			CurrentState?.Execute();

		}
	}
}