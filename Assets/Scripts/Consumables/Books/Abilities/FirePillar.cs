using UnityEngine;
using System.Collections;

namespace Consumables.Books.Abilities {
	public class FirePillar : Book {
		public override string Name => "Fire Pillar";
		public override string Description => "It places a trap which activates a fire pillar when the enemy steps on it.";
		public override string Element => "Fire";
		public override string Rarity => "Common";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.FirePillar;
		public override int Charges => 5;

		[SerializeField] private GameObject firePillarPrefab;

		private void Awake() {
			bookIcon = Resources.Load<Sprite>("Images/InfernoCommonBook");
			firePillarPrefab = Resources.Load("Prefabs/Attacks/FirePillar") as GameObject;
		}

		public override void UseConsumable() {
			// TODO: Instatiate on the ground, where player is aiming
			bookVFX = Instantiate(firePillarPrefab, transform, true);

			RemoveCharge();     // Remove charge after the ability is used
		}

		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}
	}
}