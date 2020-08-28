using UnityEngine;
using System.Collections;

namespace Consumables.Books.Abilities {
	public class WaterShield : Book {

		public override string Name => "Water Shield";
		public override string Description => "Create a water shield that protects against attacks.";
		public override EnumUtility.AttackType Element => EnumUtility.AttackType.Neptunian;
		public override string Rarity => "Common";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.WaterShield;
		public override int Charges => 5;


		private GameObject player;

		public WaterShield(Transform container) : base(container) { }

		protected override void Awaker() {
			base.Awaker();
			bookIcon = Resources.Load<Sprite>("Images/NeptunianCommonBook");
		}


		public override void UseConsumable() {

			RemoveCharge();     // Remove charge after the ability is used
		}
	}
}