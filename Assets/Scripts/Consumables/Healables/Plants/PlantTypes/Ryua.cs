using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Consumables.Healables.Plants.PlantTypes {
	public class Ryua : Plant {
		public override string Name => "Ryua";

		public override string Description => "Plant for Ryuyuki race.";

		public override EnumUtility.CharacterType Type => EnumUtility.CharacterType.Ryuyuki;

		public Ryua(Transform container) : base(container) { }

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
