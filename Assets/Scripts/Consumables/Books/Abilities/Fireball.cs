using System;
using UnityEngine;
using System.Collections;
using Characters.Interfaces;
using UnityEngine.UI;
using System.Linq;
using Photon.Pun;
using Managers;
using Object = UnityEngine.Object;

namespace Consumables.Books.Abilities {
    public class Fireball : Book {
        public override string Name => "Fireball";
        public override string Description => "Throw a ball of fire";
        public override EnumUtility.AttackType Element => EnumUtility.AttackType.Inferno;
        public override string Rarity => "Rare";
        public override EnumUtility.PageType PageType => EnumUtility.PageType.Fireball;
        public override int Charges => 3;

        [SerializeField] private GameObject fireballPrefab;
        [SerializeField] private float fireballSpeed = 5F;

        private GameObject player;

        public Fireball(Transform container) : base(container) { }

        protected override void Awaker() {
            base.Awaker();
            bookIcon = Resources.Load<Sprite>("Images/InfernoRareBook");
            fireballPrefab = Resources.Load("Prefabs/Attacks/Fireball") as GameObject;
        }


        public override void UseConsumable() {
            var players = GameObject.FindGameObjectsWithTag("Player");
            player = players.FirstOrDefault(p => p.GetComponent<PhotonView>().IsMine);
            if (Equals(player, null)) return;
            
            var camera = player.GetComponent<PlayableCharacter>().Camera;
            
            player.GetComponent<PhotonView>().RPC("RPC_ShootFireball", RpcTarget.All, camera.transform);

            RemoveCharge(); // Remove charge after the ability is used
        }

        private IEnumerator MoveFireball() {
            while (true) {
                yield return new WaitForEndOfFrame();
                
                if (!Equals(bookVFX, null)) {
                    bookVFX.transform.Translate(Vector3.forward * (Time.unscaledDeltaTime * fireballSpeed), Space.Self);
                }
            }
        }
        
#region RPC

        [PunRPC]
        private void RPC_ShootFireball(Transform transform) {
            // Define the behavior of the ability
            bookVFX = Object.Instantiate(fireballPrefab, transform.position + transform.forward * 2, transform.rotation, container);
            GameManager.Instance.StartCoroutine(MoveFireball());
        }

#endregion
    }
}