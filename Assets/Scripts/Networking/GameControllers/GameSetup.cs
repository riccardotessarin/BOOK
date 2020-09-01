using UnityEngine;

namespace Networking.GameControllers {
    public class GameSetup : MonoBehaviour {
        [SerializeField] private Transform[] spawnPoints;

        public static GameSetup instance;

        public Transform[] SpawnPoints => spawnPoints;

        private void OnEnable() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(this);
            }
        }
    }
}