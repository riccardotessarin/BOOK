using Photon.Pun;
using Test;
using UnityEngine;

namespace Level.Tests {
    public class Coordination : Trial {
        [SerializeField] protected Transform[] spawnPoints = new Transform[4];
        [SerializeField] protected TrialButton[] buttons = new TrialButton[2];


        // Start is called before the first frame update
        protected override void Starter() {
            description = "press buttons spawned in the area";
            base.Starter();
        }

        // Update is called once per frame

        public override void StartTrial() {
            start = true;
            int val = Random.Range(0, 4);
            int oldestVal = val;
            buttons[0] = PhotonNetwork.Instantiate("Prefabs/LevelUtility/Button", spawnPoints[val].position, spawnPoints[val].rotation).GetComponent<TrialButton>();
            while (val == oldestVal)
                val = Random.Range(0, 4);
            buttons[1] = PhotonNetwork.Instantiate("Prefabs/LevelUtility/Button", spawnPoints[val].position, spawnPoints[val].rotation).GetComponent<TrialButton>();
            
            base.StartTrial();
        }

        protected override void EndTrial() {
            foreach (var button in buttons) {
                if (!button.pressed)
                    return;
            }

            completed = true;
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