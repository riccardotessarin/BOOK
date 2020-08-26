using UnityEngine;
using System.Collections;
using User;
using Consumables.Books.Abilities;

namespace Consumables.Books.Drops {
	public class FireballDrop : BookDrop {

		public override string DropName => "Fireball";
		public override string DropDescription => "Throw a ball of fire";
		public override string DropElement => "Fire";
		public override string DropRarity => "Rare";
		public override EnumUtility.PageType DropPageType => EnumUtility.PageType.Fireball;
		public override int DropCharges => 3;

		private void Awake() {
			bookDrop3DModel = Resources.Load<GameObject>("");
		}

		public override void PickDrop() {
			var inventory = Inventory.Instance;
			Book fireball = new Fireball(inventory.container);
			inventory.TryAddConsumableToInventory(fireball);
			Destroy(this.gameObject);
		}

		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}
	}
}