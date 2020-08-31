using UnityEngine;
using System.Collections;
using User;
using Consumables.Books.Abilities;

namespace Consumables.Books.Drops {
	public class VenomousNeedleDrop : BookDrop {

		public override string Name => "Venomous Needle";
		public override string Description => "Shoot a toxic needle which deals damage over time.";
		public override EnumUtility.AttackType Element => EnumUtility.AttackType.Basilisk;
		public override string Rarity => "Rare";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.VenomousNeedle;
		public override int Charges => 3;

		protected override void Awaker() {
			base.Awaker();
			bookIcon = Resources.Load<Sprite>("Images/BasiliskRareBook");
			bookDrop3DModel = Resources.Load<GameObject>("");
		}

		public override bool PickDrop(Inventory inventory) {
			Book book = new VenomousNeedle(inventory.booksContainer);
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