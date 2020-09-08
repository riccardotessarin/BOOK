using UnityEngine;
using System.Collections;
using User;
using Consumables.Healables.Plants.PlantTypes;

namespace Consumables.Healables.Plants.Drops {
	public class RyuaDrop : PlantDrop {

		public override string Name => "Ryua";

		public override string Description => "Plant for Ryuyuki race.";

		public override EnumUtility.CharacterType Type => EnumUtility.CharacterType.Ryuyuki;

		public override bool PickDrop(Inventory inventory) {
			Plant plant = new Ryua(inventory.booksContainer);
			var success = inventory.TryAddConsumableToInventory(plant);
			if (success) {
				Destroy(this.gameObject);
			}
			return success;
		}

		protected override void Awaker() {
			base.Awaker();
			plantIcon = (Sprite)Resources.Load("Images/Ryua", typeof(Sprite));
		}

		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}
	}
}