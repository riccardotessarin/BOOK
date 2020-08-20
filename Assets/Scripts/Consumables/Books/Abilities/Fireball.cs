using UnityEngine;
using System.Collections;
using Characters.Interfaces;
using UnityEngine.UI;

namespace Consumables.Books.Abilities {
	public class Fireball : Book {

		public override string Name => "Fireball";
		public override string Description => "Throw a ball of fire";
		public override string Element => "Fire";
		public override string Rarity => "Rare";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.Fireball;
		public override int Charges => 3;

		[SerializeField] private GameObject FireballBookIcon;
		[SerializeField] private GameObject fireballPrefab;
		[SerializeField] private int fireballSpeed = 10;

		private void Awake() {
			bookIcon = Resources.Load<Texture>("Images/InfernoRareBook");
			fireballPrefab = Resources.Load("Prefabs/Attacks/Fireball") as GameObject;
		}

		public override void UseConsumable() {
			// Define the behavior of the ability
			bookVFX = Instantiate(fireballPrefab, transform, true);
			Rigidbody rigidbody = bookVFX.GetComponent<Rigidbody>();
			rigidbody.velocity = transform.forward * fireballSpeed;

			RemoveCharge();     // Remove charge after the ability is used
		}

		// Start is called before the first frame update
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}
	}
}
