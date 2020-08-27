using UnityEngine;
using System.Collections;
using User;
using Consumables.Books.Abilities;

namespace Consumables.Books.Drops {
	public class FireballDrop : BookDrop {

		public override string Name => "Fireball";
		public override string Description => "Throw a ball of fire";
		public override string Element => "Fire";
		public override string Rarity => "Rare";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.Fireball;
		public override int Charges => 3;

		private void Awake() {
			bookDrop3DModel = Resources.Load<GameObject>("");
		}

		public override void PickDrop() {
			var inventory = Inventory.Instance;
			Book fireball = new Fireball(inventory.booksContainer);
			var success = inventory.TryAddConsumableToInventory(fireball);
			if(success) {
				Destroy(this.gameObject);
			}
		}

		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}
	}
}