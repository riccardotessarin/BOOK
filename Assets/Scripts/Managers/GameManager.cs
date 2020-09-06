using System;
using System.Net.Security;
using Photon.Pun;
using StateMachine.States;
using UnityEngine;
using UnityEngine.Serialization;

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
			if (!_photonView.IsMine) return;

			if (PhotonNetwork.IsMasterClient) {
				InstantiateNPC();
			}
			
			CurrentState = new InGameState();
			
		}

		private void InstantiateNPC() {
			PhotonNetwork.Instantiate("Prefabs/NPC/Cyborg_Kinean", npcSpawnPoints[0].position, npcSpawnPoints[0].rotation);
			PhotonNetwork.Instantiate("Prefabs/NPC/Melting_Kinean", npcSpawnPoints[1].position, npcSpawnPoints[1].rotation);
		}

		// Update is called once per frame
		private void Update() {
			if (!_photonView.IsMine) return;
			
			CurrentState?.Execute();
		}
	}
}