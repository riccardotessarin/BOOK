using UnityEngine;
using System.Collections;
using User;
using Consumables.Books.Abilities;

namespace Consumables.Books.Drops {
	public class FirePillarDrop : BookDrop {

		public override string Name => "Fire Pillar";
		public override string Description => "It places a trap which activates a fire pillar when the enemy steps on it.";
		public override EnumUtility.AttackType Element => EnumUtility.AttackType.Inferno;
		public override string Rarity => "Common";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.FirePillar;
		public override int Charges => 5;

		private void Awake() {
			bookDrop3DModel = Resources.Load<GameObject>("");
		}

		public override bool PickDrop(Inventory inventory) {
			Book book = new FirePillar(inventory.booksContainer);
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
