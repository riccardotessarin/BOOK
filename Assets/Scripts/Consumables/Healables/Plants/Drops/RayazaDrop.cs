using UnityEngine;
using System.Collections;
using Consumables.Healables.Plants.PlantTypes;
using User;

namespace Consumables.Healables.Plants.Drops {
	public class RayazaDrop : PlantDrop {

		public override string Name => "Rayaza";

		public override string Description => "Plant for Rayaz race.";

		public override EnumUtility.CharacterType Type => EnumUtility.CharacterType.Rayaz;

		public override void PickDrop() {
			var inventory = Inventory.Instance;
			Plant genea = new Rayaza(inventory.booksContainer);
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