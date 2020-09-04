using System;
using System.IO;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Networking.GameControllers {
    public class PhotonPlayer : MonoBehaviour {
        [SerializeField] private Material controlledPlayerMaterial;

        private PhotonView _photonView;

        public GameObject MyPlayerAvatar { get; private set; }

#region Unity methods

        private void Start() {
            _photonView = GetComponent<PhotonView>();
            if (!_photonView.IsMine) return;
            
            int spawnPicker = Random.Range(0, GameSetup.instance.SpawnPoints.Length);
            MyPlayerAvatar = PhotonNetwork.Instantiate(Path.Combine("Prefabs/Player", PlayerInfo.Instance.SelectedClass),
                GameSetup.instance.SpawnPoints[spawnPicker].position, GameSetup.instance.SpawnPoints[spawnPicker].rotation, 0);

            MyPlayerAvatar.GetComponentInChildren<MeshRenderer>().material = controlledPlayerMaterial;
            
            _photonView.RPC("RPC_InstantiatePlayer", RpcTarget.OthersBuffered, PlayerInfo.Instance.SelectedClass);
        }

#endregion

#region RPC

        [PunRPC]
        private void RPC_InstantiatePlayer(string playerClass) {
            MyPlayerAvatar = InstantiatePlayer(playerClass);
        }

#endregion
        
        private GameObject InstantiatePlayer(string instanceSelectedClass) {
            var classGameObject = Resources.Load<GameObject>(Path.Combine("Prefabs/Player", instanceSelectedClass));
            return Instantiate(classGameObject, transform.position, transform.rotation, transform);
        }
    }
}