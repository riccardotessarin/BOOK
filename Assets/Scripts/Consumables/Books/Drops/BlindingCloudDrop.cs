using UnityEngine;
using System.Collections;
using User;
using Consumables.Books.Abilities;

namespace Consumables.Books.Drops {
	public class BlindingCloudDrop : BookDrop {

		public override string Name => "Blinding Cloud";
		public override string Description => "A toxic cloud that blinds enemies and allies in a certain radius.";
		public override EnumUtility.AttackType Element => EnumUtility.AttackType.Basilisk;
		public override string Rarity => "Common";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.BlindingCloud;
		public override int Charges => 5;

		protected override void Awaker() {
			base.Awaker();
			bookIcon = Resources.Load<Sprite>("Images/BasiliskCommonBook");
			bookDrop3DModel = Resources.Load<GameObject>("");
		}

		public override bool PickDrop(Inventory inventory) {
			Book book = new BlindingCloud(inventory.booksContainer);
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