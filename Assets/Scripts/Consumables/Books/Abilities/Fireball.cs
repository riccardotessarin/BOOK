using UnityEngine;
using System.Collections;
using Characters.Interfaces;
using UnityEngine.UI;
using System.Linq;
using Photon.Pun;

namespace Consumables.Books.Abilities {
	public class Fireball : Book {

		public override string Name => "Fireball";
		public override string Description => "Throw a ball of fire";
		public override string Element => "Fire";
		public override string Rarity => "Rare";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.Fireball;
		public override int Charges => 3;

		[SerializeField] private GameObject fireballPrefab;
		[SerializeField] private float fireballSpeed = 0.1F;

		private GameObject player;

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
			// Define the behavior of the ability
			
			bookVFX = Instantiate(fireballPrefab, playerTransform.position, playerTransform.rotation);
			bookVFX.transform.parent = transform;
			StartCoroutine(MoveFireball());

			RemoveCharge();     // Remove charge after the ability is used
		}

		private IEnumerator MoveFireball() {
			while(true) {
				yield return new WaitForEndOfFrame();
				var fireballPosition = bookVFX.transform.position;
				bookVFX.transform.position = fireballPosition + Vector3.forward * fireballSpeed;
			}

		}

		#region UnityMethods
		// Start is called before the first frame update
		void Start() {
			
		}

		// Update is called once per frame
		void Update() {

		}
		#endregion
	}
}
