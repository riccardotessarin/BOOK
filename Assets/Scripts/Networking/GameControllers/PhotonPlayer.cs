using System.IO;
using Characters.Interfaces;
using Photon.Pun;
using UnityEngine;

namespace Networking.GameControllers {
    public class PhotonPlayer : MonoBehaviour {
        private PlayableCharacter MyPlayerAvatar { get; set; }

        public PhotonView PhotonView { get; private set; }

#region Unity methods

        private void Awake() {
            PhotonView = GetComponent<PhotonView>();
        }

        private void Start() {
            if (!PhotonView.IsMine) return;

            int spawnPicker = Random.Range(0, GameSetup.instance.SpawnPoints.Length);
            var player = PhotonNetwork.Instantiate(Path.Combine("Prefabs/Player", PlayerInfo.Instance.SelectedClass),
                GameSetup.instance.SpawnPoints[spawnPicker].position, GameSetup.instance.SpawnPoints[spawnPicker].rotation, 0);

            MyPlayerAvatar = player.GetComponent<PlayableCharacter>();
            MyPlayerAvatar.Player = this;
            
            PhotonView.RPC("RPC_SavePlayerInstance", RpcTarget.OthersBuffered, player.GetComponent<PhotonView>().ViewID);
        }

#endregion

#region RPC

        [PunRPC]
        private void RPC_BasicAttack(Vector3 position, Vector3 direction) {
            if (MyPlayerAvatar == null) {
                return;
            }
            
            StartCoroutine(MyPlayerAvatar.BaseAttackDamage(position, direction));
        }
        
        [PunRPC]
        private void RPC_SavePlayerInstance(int playerID) {
            PhotonView.Find(playerID);
            MyPlayerAvatar = PhotonView.Find(playerID)?.GetComponent<PlayableCharacter>();
        }

#endregion

        private GameObject InstantiatePlayer(string instanceSelectedClass) {
            var classGameObject = Resources.Load<GameObject>(Path.Combine("Prefabs/Player", instanceSelectedClass));
            return Instantiate(classGameObject, transform.position, transform.rotation, transform);
        }
    }
}