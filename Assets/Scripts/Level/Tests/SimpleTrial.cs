using Photon.Pun;
using Test;
using UnityEngine;

namespace Level.Tests {
    public class SimpleTrial : Trial {
        protected override void Starter() {
            base.Starter();
            description = "move to the next area after interacted with this";
        }

        // Start is called before the first frame update
        public override void StartTrial() {
            start = true;
            
            base.StartTrial();
        }

        protected override void EndTrial() {
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
            //ended = true;
            GetComponent<Collider>().enabled = false;
        }

#endregion
    }
}