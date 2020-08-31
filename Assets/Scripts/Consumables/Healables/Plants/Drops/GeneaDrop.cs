using UnityEngine;
using System.Collections;
using User;
using Consumables.Healables.Plants.PlantTypes;

namespace Consumables.Healables.Plants.Drops {
	public class GeneaDrop : PlantDrop {

		public override string Name => "Genea";

		public override string Description => "Plant for Genee race.";

		public override EnumUtility.CharacterType Type => EnumUtility.CharacterType.Genee;

		public override bool PickDrop(Inventory inventory) {
			Plant plant = new Genea(inventory.booksContainer);
			var success = inventory.TryAddConsumableToInventory(plant);
			if(success) {
				Destroy(this.gameObject);
			}
			return success;
		}

		protected override void Awaker() {
			base.Awaker();
			plantIcon = (Sprite)Resources.LoadAll("Images/pngwing.com", typeof(Sprite))[0];
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