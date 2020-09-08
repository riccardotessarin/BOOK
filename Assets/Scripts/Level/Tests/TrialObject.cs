using Photon.Pun;
using UnityEngine;

namespace Level.Tests {
    public class TrialObject : MonoBehaviour {
        [SerializeField] protected bool collected;

        private PhotonView _photonView;

        private void Awake() {
            _photonView = GetComponent<PhotonView>();
        }

        public bool Collected => collected;

        public void Collect() {
            _photonView.RPC("RPC_Collect", RpcTarget.AllBuffered);
        }

#region RPC

        [PunRPC]
        private void RPC_Collect() {
            collected = true;
            gameObject.SetActive(false);
        }

#endregion
    }
}