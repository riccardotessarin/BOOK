using UnityEngine;
using System.Collections;
using User;
using Consumables.Books.Abilities;

namespace Consumables.Books.Drops {
	public class SurgingTideDrop : BookDrop {

		public override string Name => "Surging Tide";
		public override string Description => "Evoke a tide that drives all enemies away, without dealing damage.";
		public override EnumUtility.AttackType Element => EnumUtility.AttackType.Neptunian;
		public override string Rarity => "Rare";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.SurgingTide;
		public override int Charges => 3;

		private void Awake() {
			bookDrop3DModel = Resources.Load<GameObject>("");
		}

		public override bool PickDrop(Inventory inventory) {
			Book book = new SurgingTide(inventory.booksContainer);
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