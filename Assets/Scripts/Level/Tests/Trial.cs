using System.Collections;
using System.Collections.Generic;
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

        public abstract void StartTrial();
        protected abstract void EndTrial();

        protected virtual void TrialCompleted() {
            if (!lastTrial)
                walls.SetActive(false);
            else
                SpawnLibraryDoor();
            
            Debug.Log("Access to next area");
            ended = true;
            GetComponent<Collider>().enabled = false;
        }

        protected virtual void SpawnLibraryDoor() {
            library.SetActive(true);
        }
    }
}