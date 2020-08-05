using UnityEngine;
using System.Collections;
using Characters.Interfaces;


namespace Consumables.Books.Abilities {
	public class Fireball : Book {

		public override string Name => "Fireball";
		public override string Description => "Throw a ball of fire";
		public override string Element => "Fire";
		public override string Rarity => "Rare";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.Fireball;
		public override int Charges => 3;



		public override int UseConsumable() {
			// Define the behavior of the ability


			RemoveCharge();     // Remove charge after the ability is used
			return 0;           // Return attack damage
		}

		// Start is called before the first frame update
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}

		private void OnTriggerEnter(Collider other) {
			if (other.gameObject.GetComponent<NPC>() != null) {

			}
		}
	}
}
