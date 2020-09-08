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

            if (!playableCharacter.IsDeath && !playableCharacter.IsReanimating && !playableCharacter.UIManager.ChatMode)
                base.FixedUpdate();

            _photonView.RPC("RPC_SetOtherPlayerMovementInfo", RpcTarget.Others, speedToTransmit, IsJumping, IsWalking, RunSpeed, WalkingSpeed, moveDir);
        }

        protected override void OnControllerColliderHit(ControllerColliderHit hit) {
            if (!_photonView.IsMine) {
                return;
            }

            base.OnControllerColliderHit(hit);
        }

#endregion

#region RPC
        
        [PunRPC]
        private void RPC_SetOtherPlayerMovementInfo(float speedToTransmit, bool isJumping, bool isWalking, float runSpeed, float walkingSpeed, Vector2 moveDir) {
            this.speedToTransmit = speedToTransmit;
            IsJumping = isJumping;
            m_IsWalking = isWalking;
            m_RunSpeed = runSpeed;
            this.moveDir = moveDir;
        }

#endregion
    }
}