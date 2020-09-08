using System.IO;
using Networking.GameControllers;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Networking {
    public class PhotonRoom : MonoBehaviourPunCallbacks {
        [SerializeField] private int multiplayerScene;

        private PhotonView _photonView;
        private int _currentScene;

        public static PhotonRoom Instance { get; private set; }

#region Unity methods

        private void Awake() {
            if (Instance == null || ReferenceEquals(this, Instance)) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(this);
            }
        }

        private void Start() {
            _photonView = GetComponent<PhotonView>();
        }

        public override void OnEnable() {
            base.OnEnable();
            PhotonNetwork.AddCallbackTarget(this);
            SceneManager.sceneLoaded += OnSceneFinishedLoading;
        }

        public override void OnDisable() {
            base.OnDisable();
            PhotonNetwork.RemoveCallbackTarget(this);
            SceneManager.sceneLoaded -= OnSceneFinishedLoading;
        }

#endregion

#region Callbacks

        private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode) {
            _currentScene = scene.buildIndex;

            if (_currentScene == multiplayerScene) {
                CreatePlayer();
            }
        }

#endregion

#region Photon callbacks

        public override void OnJoinedRoom() {
            Debug.LogWarning($"Joined: {PhotonNetwork.CurrentRoom.Name}");

            StartGame();
        }

#endregion

        private void StartGame() {
            if (!PhotonNetwork.IsMasterClient)
                return;

            PhotonNetwork.LoadLevel(multiplayerScene);
        }

        private void CreatePlayer() {
            var player = PhotonNetwork.Instantiate(Path.Combine("Prefabs/Player", "Player"), transform.position, Quaternion.identity, 0);
            player.GetComponent<PhotonPlayer>().Username = PhotonLobby.Instance.Username;
        }
    }
}