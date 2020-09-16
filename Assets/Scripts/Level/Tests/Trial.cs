using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Test {
    public abstract class Trial : MonoBehaviour {
        [SerializeField] protected string description;
        public string Description => description;
        [SerializeField] protected bool start;
        public bool Starting => start;
        [SerializeField] protected bool completed;
        public bool Completed => completed;
        [SerializeField] protected GameObject walls;
        [SerializeField] protected bool ended;
        public bool Ended => ended;
        [SerializeField] protected bool lastTrial;
        [SerializeField] protected GameObject library;
        
        protected PhotonView photonView;

        private void Awake() {
            photonView = GetComponent<PhotonView>();
        }

        // Start is called before the first frame update
        void Start() {
            Starter();
        }

        protected virtual void Starter() {
            start = false;
            completed = false;
            ended = false;
        }

        // Update is called once per frame
        void Update() {
            Updater();
        }

        protected virtual void Updater() {
            if (!ended && start) {
                if (!completed) {
                    EndTrial();
                } else {
                    TrialCompleted();
                }
            }
        }

        public virtual void StartTrial() {
            photonView.RPC("RPC_StartTrial", RpcTarget.AllBuffered);
        }

        protected virtual void EndTrial() {
            photonView.RPC("RPC_EndTrial", RpcTarget.AllBuffered);
        }

        private void TrialCompleted() {
            ended=true;
            if (!lastTrial)
                walls.transform.localScale = Vector3.zero;
            else
                SpawnLibraryDoor();
            
            Debug.Log("Access to next area");
                        
            photonView.RPC("RPC_TrialCompleted", RpcTarget.AllBuffered);
        }

        private void SpawnLibraryDoor() {
            library.transform.localScale = Vector3.one;
        }
    }
}