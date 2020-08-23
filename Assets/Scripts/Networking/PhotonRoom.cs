using System;
using System.IO;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Networking {
    public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks {
        [SerializeField] private int multiplayerScene;
        
        private PhotonView _photonView;
        private int _currentScene;
    
        public static PhotonRoom Instance { get; private set; }

#region Unity methods

        private void Awake() {
            if (Instance != null && !Equals(Instance)) {
                Destroy(Instance.gameObject);
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
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
            Debug.Log("Joined a room");

            StartGame();
        }
        
#endregion
        
        private void StartGame() {
            if (!PhotonNetwork.IsMasterClient) 
                return;

            PhotonNetwork.LoadLevel(multiplayerScene);
        }
        
        private void CreatePlayer() {
            PhotonNetwork.Instantiate(Path.Combine("Prefabs/Player", "RyuyukiPlayer"), transform.position, Quaternion.identity, 0);
        }
    }
}