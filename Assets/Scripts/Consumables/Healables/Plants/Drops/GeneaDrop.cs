using UnityEngine;
using System.Collections;
using User;
using Consumables.Healables.Plants.PlantTypes;

namespace Consumables.Healables.Plants.Drops {
	public class GeneaDrop : PlantDrop {

		public override string Name => "Genea";

		public override string Description => "Plant for Genee race.";

		public override EnumUtility.CharacterType Type => EnumUtility.CharacterType.Genee;

		public override void PickDrop() {
			var inventory = Inventory.Instance;
			Plant genea = new Genea(inventory.booksContainer);
			inventory.TryAddConsumableToInventory(genea);
			Destroy(this.gameObject);
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