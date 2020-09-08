using System;
using Characters.Interfaces;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level {
    public class TriggerDoor : MonoBehaviour {
        private PhotonView _photonView;

        private void Awake() {
            _photonView = GetComponent<PhotonView>();
        }

        private void OnTriggerEnter(Collider collider) {
            if (collider.GetComponent<PlayableCharacter>()) {
                _photonView.RPC("RPC_WinFunction", RpcTarget.AllBuffered);
            }
        }

#region RPC

        [PunRPC]
        private void RPC_WinFunction() {
            Debug.Log("You win");
            SceneManager.LoadScene("MatchMakingScene");
        }

#endregion
    }
}