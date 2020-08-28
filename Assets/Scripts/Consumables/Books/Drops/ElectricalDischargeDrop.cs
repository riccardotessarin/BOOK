using UnityEngine;
using System.Collections;
using User;
using Consumables.Books.Abilities;

namespace Consumables.Books.Drops {
	public class ElectricalDischargeDrop : BookDrop {

		public override string Name => "Electrical Discharge";
		public override string Description => "Evoke an electric discharge which links to nearby enemies, stunning them and dealing damage.";
		public override EnumUtility.AttackType Element => EnumUtility.AttackType.Raijin;
		public override string Rarity => "Rare";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.ElectricalDischarge;
		public override int Charges => 3;

		private void Awake() {
			bookDrop3DModel = Resources.Load<GameObject>("");
		}

		public override bool PickDrop(Inventory inventory) {
			Book book = new ElectricalDischarge(inventory.booksContainer);
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