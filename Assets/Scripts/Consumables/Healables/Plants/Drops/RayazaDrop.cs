using UnityEngine;
using System.Collections;
using Consumables.Healables.Plants.PlantTypes;
using User;

namespace Consumables.Healables.Plants.Drops {
	public class RayazaDrop : PlantDrop {

		public override string Name => "Rayaza";

		public override string Description => "Plant for Rayaz race.";

		public override EnumUtility.CharacterType Type => EnumUtility.CharacterType.Rayaz;

		public override bool PickDrop(Inventory inventory) {
			Plant plant = new Rayaza(inventory.booksContainer);
			var success = inventory.TryAddConsumableToInventory(plant);
			if(success) {
				Destroy(this.gameObject);
			}
			return success;
		}

		protected override void Awaker() {
			base.Awaker();
			plantIcon = (Sprite)Resources.LoadAll("Images/pngwing.com", typeof(Sprite))[1];
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