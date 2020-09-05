using System.IO;
using Photon.Pun;
using UnityEngine;

namespace Networking.GameControllers {
    public class PhotonPlayer : MonoBehaviour {
        [SerializeField] private Material controlledPlayerMaterial;

        private PhotonView _photonView;

        public GameObject MyPlayerAvatar { get; private set; }

#region Unity methods

        private void Awake() {
            _photonView = GetComponent<PhotonView>();
        }

        private void Start() {
            if (!_photonView.IsMine) return;

            int spawnPicker = Random.Range(0, GameSetup.instance.SpawnPoints.Length);
            MyPlayerAvatar = PhotonNetwork.Instantiate(Path.Combine("Prefabs/Player", PlayerInfo.Instance.SelectedClass),
                GameSetup.instance.SpawnPoints[spawnPicker].position, GameSetup.instance.SpawnPoints[spawnPicker].rotation, 0);

            MyPlayerAvatar.GetComponentInChildren<MeshRenderer>().material = controlledPlayerMaterial;
        }

#endregion

        private GameObject InstantiatePlayer(string instanceSelectedClass) {
            var classGameObject = Resources.Load<GameObject>(Path.Combine("Prefabs/Player", instanceSelectedClass));
            return Instantiate(classGameObject, transform.position, transform.rotation, transform);
        }
    }
}