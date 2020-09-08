using Photon.Pun;
using Test;
using UnityEngine;

namespace Level.Tests {
    public class BloodAltar : Trial {
        [SerializeField] protected float lifeToPay = 30;
        [SerializeField] protected float lifePaid = 0;

        protected override void Starter() {
            base.Starter();
            description = "pay life to pass. Paid life: " + lifePaid.ToString();
        }

        protected override void Updater() {
            base.Updater();
            description = "pay life to pass. Paid life: " + lifePaid.ToString();
        }

        public override void StartTrial() {
            start = true;
            
            base.StartTrial();
        }

        protected override void EndTrial() {
            if (lifePaid < lifeToPay)
                return;
            completed = true;
            
            base.EndTrial();
        }

        public void AddLife(float life) {
            photonView.RPC("RPC_SyncLifePaid", RpcTarget.AllBuffered, life);
        }

#region RPC

        [PunRPC]
        private void RPC_SyncLifePaid(float life) {
            lifePaid += life;
        }

        [PunRPC]
        private void RPC_StartTrial() {
            start = true;
        }

        [PunRPC]
        private void RPC_EndTrial() {
            completed = true;
        }

        [PunRPC]
        private void RPC_TrialCompleted() {
            ended = true;
            GetComponent<Collider>().enabled = false;
        }

#endregion
    }
}