using Characters.NPC;
using Photon.Pun;
using Test;
using UnityEngine;

namespace Level.Tests {
    public class BloodBath : Trial {
        [SerializeField] protected Transform[] spawnPoints = new Transform[3];
        [SerializeField] protected CyborgKinean[] enemies = new CyborgKinean[3];

        protected override void Starter() {
            base.Starter();
            description = "Destroy 3 Cyborg_Kineans that will spawn in this area";
        }

        public override void StartTrial() {
            Debug.Log("Trial Started");
            
            for (int i = 0; i < spawnPoints.Length; i++) {
                enemies[i] = PhotonNetwork.Instantiate("Prefabs/NPC/Cyborg_Kinean", spawnPoints[i].position, spawnPoints[i].rotation).GetComponent<CyborgKinean>();
            }

            base.StartTrial();
        }

        protected override void EndTrial() {
            for (int i = 0; i < enemies.Length; i++) {
                if (!enemies[i].IsDeath)
                    return;
            }
            
            Debug.Log("Trial Completed");

            base.EndTrial();
        }

#region RPC

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