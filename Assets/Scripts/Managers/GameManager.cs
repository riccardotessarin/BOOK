using Photon.Pun;
using StateMachine.States;
using UnityEngine;

namespace Managers {
	public class GameManager : MonoBehaviour {
		[SerializeField] private Transform[] npcSpawnPoints;
		
		private PhotonView _photonView;

		public static GameManager Instance { get; private set; }
		public State CurrentState { get; set; }

		private void Awake() {
			if (Instance == null) {
				Instance = this;
			} else {
				Destroy(this);
			}

			_photonView = GetComponent<PhotonView>();
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