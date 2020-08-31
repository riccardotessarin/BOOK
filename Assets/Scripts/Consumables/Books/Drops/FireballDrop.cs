using UnityEngine;
using System.Collections;
using User;
using Consumables.Books.Abilities;

namespace Consumables.Books.Drops {
	public class FireballDrop : BookDrop {

		public override string Name => "Fireball";
		public override string Description => "Throw a ball of fire";
		public override EnumUtility.AttackType Element => EnumUtility.AttackType.Inferno;
		public override string Rarity => "Rare";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.Fireball;
		public override int Charges => 3;

		protected override void Awaker() {
			base.Awaker();
			bookIcon = Resources.Load<Sprite>("Images/InfernoRareBook");
			bookDrop3DModel = Resources.Load<GameObject>("");
		}

		public override bool PickDrop(Inventory inventory) {
			Book book = new Fireball(inventory.booksContainer);
			var success = inventory.TryAddConsumableToInventory(book);
			if(success) {
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