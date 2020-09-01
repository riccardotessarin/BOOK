using System.IO;
using Photon.Pun;
using UnityEngine;

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
            
            MyPlayerAvatar = PhotonNetwork.Instantiate(Path.Combine("Prefabs/Player", "RyuyukiPlayer"),
                GameSetup.instance.SpawnPoints[spawnPicker].position, GameSetup.instance.SpawnPoints[spawnPicker].rotation, 0);

            MyPlayerAvatar.GetComponentInChildren<MeshRenderer>().material = controlledPlayerMaterial;
        }

#endregion
    }
}