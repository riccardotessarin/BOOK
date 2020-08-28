using UnityEngine;
using System.Collections;
using Characters.Interfaces;
using UnityEngine.UI;
using System.Linq;
using Photon.Pun;
using Managers;

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

		public Fireball(Transform container): base(container) { }

		protected override void Awaker() {
			base.Awaker();
			bookIcon = Resources.Load<Sprite>("Images/InfernoRareBook");
			fireballPrefab = Resources.Load("Prefabs/Attacks/Fireball") as GameObject;
			
		}


		public override void UseConsumable() {
			var players = GameObject.FindGameObjectsWithTag("Player");
			//player = players.FirstOrDefault(player => player.GetComponent<PhotonView>().IsMine);
			player = players.FirstOrDefault();
			var playerTransform = player.transform;
			//var playerPosition = new Vector3(player.transform.localPosition.x + 4, player.transform.position.y, player.transform.position.z);
			// Define the behavior of the ability
			bookVFX = Object.Instantiate(fireballPrefab, playerTransform.position + playerTransform.forward * 2, playerTransform.rotation);
			bookVFX.transform.parent = container;
			GameManager.Instance.StartCoroutine(MoveFireball());
			RemoveCharge();     // Remove charge after the ability is used
		}

		private IEnumerator MoveFireball() {
			while(true) {
				yield return new WaitForEndOfFrame();
				bookVFX.transform.Translate(Vector3.forward * Time.unscaledDeltaTime * fireballSpeed, Space.Self);
			}
		}
	}
}
