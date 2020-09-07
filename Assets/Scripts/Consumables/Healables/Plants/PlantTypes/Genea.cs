using UnityEngine;
using System.Collections;

namespace Consumables.Healables.Plants.PlantTypes {
	public class Genea : Plant {
		public override string Name => "Genea";

		public override string Description => "Plant for Genee race.";

		public override EnumUtility.CharacterType Type => EnumUtility.CharacterType.Genee;

		public Genea(Transform container) : base(container) { }

		protected override void Awaker() {
			base.Awaker();
			plantIcon = (Sprite)Resources.Load("Images/Genea", typeof(Sprite));
		}

		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}
	}
}