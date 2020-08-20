using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Consumables.Healables.Plants.PlantTypes {
	public class Ryua : Plant {
		public override string Name => "Ryua";

		public override string Description => "Plant for Ryuyuki race.";

		public override EnumUtility.CharacterType Type => EnumUtility.CharacterType.Ryuyuki;
		void Awake(){
			plantIcon=(Sprite)Resources.LoadAll("Images/pngwing.com",typeof(Sprite))[2];
		}

		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}
	}
}
