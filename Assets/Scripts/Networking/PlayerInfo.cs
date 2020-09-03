using System;
using UnityEngine;

namespace Networking {
    public class PlayerInfo : MonoBehaviour {
        public static PlayerInfo Instance { get; private set; }

        public string SelectedClass { get; set; }

        private void Awake() {
            if (Instance == null || ReferenceEquals(this, Instance)) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(this);
            }
        }

        private void Start() {
            
        }
    }
}