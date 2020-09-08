using Photon.Pun;
using UnityEngine;

namespace Level.Tests {
    public class TrialButton : MonoBehaviour {
        private PhotonView _photonView;
        
        public bool pressed;

        private void Awake() {
            _photonView = GetComponent<PhotonView>();
        }

        // Start is called before the first frame update
        private void Start() {
            pressed = false;
        }

        // Update is called once per frame
        public void PressButton() {
           _photonView.RPC("RPC_PressButton", RpcTarget.AllBuffered);
        }

#region RPC

        [PunRPC]
        private void RPC_PressButton() {
            pressed = true;
            GetComponent<Collider>().enabled = false;
        }

#endregion
    }
}