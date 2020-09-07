using Photon.Pun;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace Networking.GameControllers {
    public class PlayerMovement : FirstPersonController {
        private PhotonView _photonView;

#region Unity methods
        
        protected override void Start() {
            _photonView = GetComponent<PhotonView>();
            
            if (!_photonView.IsMine) {
                return;
            }

            base.Start();
        }

        protected override void Update() {
            if (!_photonView.IsMine) {
                return;
            }
            base.Update();
        }

        protected override void FixedUpdate() {
            if (!_photonView.IsMine) {
                return;
            }
            if(!playableCharacter.IsDeath && !playableCharacter.IsReanimating && !playableCharacter.UIManager.ChatMode)
                base.FixedUpdate();
        }

        protected override void OnControllerColliderHit(ControllerColliderHit hit) {
            if (!_photonView.IsMine) {
                return;
            }
            
            base.OnControllerColliderHit(hit);
        }

#endregion
    }
}