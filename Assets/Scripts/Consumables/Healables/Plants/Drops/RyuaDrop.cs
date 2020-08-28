using UnityEngine;
using System.Collections;
using User;
using Consumables.Healables.Plants.PlantTypes;

namespace Consumables.Healables.Plants.Drops {
	public class RyuaDrop : PlantDrop {

		public override string Name => "Ryua";

		public override string Description => "Plant for Ryuyuki race.";

		public override EnumUtility.CharacterType Type => EnumUtility.CharacterType.Ryuyuki;

		public override void PickDrop() {
			var inventory = Inventory.Instance;
			Plant genea = new Ryua(inventory.booksContainer);
			var success = inventory.TryAddConsumableToInventory(genea);
			if (success) {
				Destroy(this.gameObject);
			}
		}

		void Awake() {
			plantDrop3DModel = Resources.Load<GameObject>("");
		}

		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}
	}
}