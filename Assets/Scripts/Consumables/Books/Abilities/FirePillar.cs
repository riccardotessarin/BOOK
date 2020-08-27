using UnityEngine;
using System.Collections;
using System.Linq;
using Managers;

namespace Consumables.Books.Abilities {
	public class FirePillar : Book {
		public override string Name => "Fire Pillar";
		public override string Description => "It places a trap which activates a fire pillar when the enemy steps on it.";
		public override EnumUtility.AttackType Element => EnumUtility.AttackType.Inferno;
		public override string Rarity => "Common";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.FirePillar;
		public override int Charges => 5;

		[SerializeField] private GameObject firePillarPrefab;
		private GameObject player;


		public FirePillar(Transform container) : base(container) {	}

		protected override void Awaker() {
			base.Awaker();
			bookIcon = Resources.Load<Sprite>("Images/InfernoCommonBook");
			firePillarPrefab = Resources.Load("Prefabs/Attacks/FirePillar") as GameObject;
		}

		public override void UseConsumable() {
			// TODO: Instatiate on the ground, where player is aiming
			var players = GameObject.FindGameObjectsWithTag("Player");
			//player = players.FirstOrDefault(player => player.GetComponent<PhotonView>().IsMine);
			player = players.FirstOrDefault();
			var playerTransform = player.transform;
			//var playerPosition = new Vector3(player.transform.localPosition.x + 4, player.transform.position.y, player.transform.position.z);
			// Define the behavior of the ability
			bookVFX = Object.Instantiate(firePillarPrefab, playerTransform.position + playerTransform.forward * 2, playerTransform.rotation);
			bookVFX.transform.parent = container;
			//GameManager.Instance.StartCoroutine(PlacePillar());
			RemoveCharge();     // Remove charge after the ability is used
		}
	}
}