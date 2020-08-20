using UnityEngine;
using System.Collections;

namespace Consumables.Healables.Plants.PlantTypes {
	public class Genea : Plant {
		public override string Name => "Genea";

		public override string Description => "Plant for Genee race.";

		public override EnumUtility.CharacterType Type => EnumUtility.CharacterType.Genee;
		void Awake(){
			plantIcon=(Sprite)Resources.LoadAll("Images/pngwing.com",typeof(Sprite))[0];
		}

		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}
	}
}