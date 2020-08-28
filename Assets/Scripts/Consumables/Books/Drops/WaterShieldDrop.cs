using UnityEngine;
using System.Collections;
using User;
using Consumables.Books.Abilities;

namespace Consumables.Books.Drops {
	public class WaterShieldDrop : BookDrop {

		public override string Name => "Water Shield";
		public override string Description => "Create a water shield that protects against attacks.";
		public override EnumUtility.AttackType Element => EnumUtility.AttackType.Neptunian;
		public override string Rarity => "Common";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.WaterShield;
		public override int Charges => 5;

		private void Awake() {
			bookDrop3DModel = Resources.Load<GameObject>("");
		}

		public override bool PickDrop(Inventory inventory) {
			Book book = new WaterShield(inventory.booksContainer);
			var success = inventory.TryAddConsumableToInventory(book);
			if (success) {
				Destroy(this.gameObject);
			}
			return success;
		}

		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}
	}
}