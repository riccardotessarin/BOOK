using UnityEngine;
using System.Collections;
using User;
using Consumables.Books.Abilities;

namespace Consumables.Books.Drops {
	public class IceStalagmiteDrop : BookDrop {

		public override string Name => "Ice Stalagmite";
		public override string Description => "Set a trap that create a stalagmite of ice";
		public override EnumUtility.AttackType Element => EnumUtility.AttackType.Niflheim;
		public override string Rarity => "Common";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.IceStalagmite;
		public override int Charges => 3;

		private void Awake() {
			bookDrop3DModel = Resources.Load<GameObject>("");
		}

		public override void PickDrop() {
			var inventory = Inventory.Instance;
			Book book = new IceStalagmite(inventory.booksContainer);
			var success = inventory.TryAddConsumableToInventory(book);
			if (success) {
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