using System.Collections;

using UnityEngine;

namespace Consumables.Healables.Plants.PlantTypes {
	public class Rayaza : Plant {
		public override string Name => "Rayaza";

		public override string Description => "Plant for Rayaz race.";

		public override EnumUtility.CharacterType Type => EnumUtility.CharacterType.Rayaz;
		void Awake(){
			plantIcon=(Sprite)Resources.LoadAll("Images/pngwing.com",typeof(Sprite))[1];
		}

		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}
	}
}
