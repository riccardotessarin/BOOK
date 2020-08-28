using UnityEngine;
using System.Collections;

namespace Consumables.Books.Abilities {
	public class VenomousNeedle : Book {

		public override string Name => "Venomous Needle";
		public override string Description => "Shoot a toxic needle which deals damage over time.";
		public override EnumUtility.AttackType Element => EnumUtility.AttackType.Basilisk;
		public override string Rarity => "Rare";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.VenomousNeedle;
		public override int Charges => 3;


		private GameObject player;

		public VenomousNeedle(Transform container) : base(container) { }

		protected override void Awaker() {
			base.Awaker();
			bookIcon = Resources.Load<Sprite>("Images/BasiliskRareBook");
		}


		public override void UseConsumable() {

			RemoveCharge();     // Remove charge after the ability is used
		}
	}
}