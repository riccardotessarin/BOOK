using System.Collections;
using System.Linq;
using Characters.Interfaces;
using Managers;
using Photon.Pun;
using UnityEngine;

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

            var cameraTransform = player.GetComponent<PlayableCharacter>().Camera.transform;
            bookVFX = PhotonNetwork.Instantiate("Prefabs/Attacks/Fireball", cameraTransform.position + cameraTransform.forward * 2, cameraTransform.rotation);
            bookVFX.transform.parent = container;
            
            GameManager.Instance.StartCoroutine(MoveFireball());
            // player.GetComponent<PhotonView>().RPC("RPC_ShootFireball", RpcTarget.All, cameraTransform.position, cameraTransform.rotation, cameraTransform.forward);

            RemoveCharge(); // Remove charge after the ability is used
        }

        private IEnumerator MoveFireball() {
            while (true) {
                yield return new WaitForEndOfFrame();

                if (bookVFX != null) {
                    bookVFX.transform.Translate(Vector3.forward * (Time.unscaledDeltaTime * fireballSpeed), Space.Self);
                }
            }
        }
    }
}