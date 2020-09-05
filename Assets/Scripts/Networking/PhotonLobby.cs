﻿using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Networking {
    public class PhotonLobby : MonoBehaviourPunCallbacks {
        [SerializeField] private Button findGameButton;
        [SerializeField] private Button cancelButton;
        [SerializeField] private TextMeshProUGUI classText;

        public static PhotonLobby Instance { get; private set; }

#region Unity methods

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this);
            }
        }

        // Start is called before the first frame update
        private void Start() {
            PhotonNetwork.ConnectUsingSettings(); //Connects to master photon server
        }

#endregion

#region Button callbacks

        public void OnFindGameButtonClick() {
            findGameButton.interactable = false;
            cancelButton.interactable = true;

            PhotonNetwork.JoinRandomRoom();
        }

        public void OnCancelButtonClick() {
            findGameButton.interactable = true;
            cancelButton.interactable = false;

            PhotonNetwork.LeaveRoom();
        }

        public void OnGeneeButtonClick() {
            classText.text = EnumUtility.PlayerClass.GeneePlayer.ToString().Replace("Player", "");
            PlayerInfo.Instance.SelectedClass = EnumUtility.PlayerClass.GeneePlayer.ToString();
        }
        
        public void OnRayazButtonClick() {
            classText.text = EnumUtility.PlayerClass.RayazPlayer.ToString().Replace("Player", "");
            PlayerInfo.Instance.SelectedClass = EnumUtility.PlayerClass.RayazPlayer.ToString();
        }
        
        public void OnRyuyukiButtonClick() {
            classText.text = EnumUtility.PlayerClass.RyuyukiPlayer.ToString().Replace("Player", "");
            PlayerInfo.Instance.SelectedClass = EnumUtility.PlayerClass.RyuyukiPlayer.ToString();
        }

#endregion

#region Photon callbacks

        public override void OnConnectedToMaster() {
            Debug.Log("Player has successfully connected to Photon master server");
            PhotonNetwork.AutomaticallySyncScene = true;
            findGameButton.interactable = true;
        }

        public override void OnJoinRandomFailed(short returnCode, string message) {
            Debug.Log("Tried to join a random game but failed. There must be no open games available");

            CreateRoom();
        }

        public override void OnCreateRoomFailed(short returnCode, string message) {
            Debug.Log("Tried to create a room but failed. There must already be a room with the same name");

            CreateRoom();
        }

#endregion

        private static void CreateRoom() {
            Debug.Log("Trying to create a new room");

            int randomRoomName = Random.Range(0, 10000);
            var roomOptions = new RoomOptions() {
                IsVisible = true,
                IsOpen = true,
                MaxPlayers = 4
            };

            PhotonNetwork.CreateRoom($"Room {randomRoomName}", roomOptions);
            Debug.LogWarning($"Room name: {PhotonNetwork.CurrentRoom.Name}");
        }
    }
}