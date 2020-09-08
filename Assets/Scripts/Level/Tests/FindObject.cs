using System.Collections.Generic;
using Photon.Pun;
using Test;
using UnityEngine;

namespace Level.Tests {
    public class FindObject : Trial {
        [SerializeField] protected List<Transform> spawnPoints = new List<Transform>();
        [SerializeField] protected TrialObject crystal;

        protected override void Starter() {
            base.Starter();
            description = "find the crystal";
            //Load prefab
        }

        public override void StartTrial() {
            start = true;
            crystal = PhotonNetwork.Instantiate("Prefabs/LevelUtility/Crystal", spawnPoints[Random.Range(0, spawnPoints.Count)].position, 
                spawnPoints[Random.Range(0, spawnPoints.Count)].rotation).GetComponent<TrialObject>();
            
            base.StartTrial();
        }

        protected override void EndTrial() {
            if (!crystal.Collected)
                return;
            completed = true;
            
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