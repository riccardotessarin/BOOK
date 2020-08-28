using UnityEngine;
using System.Collections;

namespace Consumables.Books.Abilities {
	public class BlindingCloud : Book {

		public override string Name => "Blinding Cloud";
		public override string Description => "A toxic cloud that blinds enemies and allies in a certain radius.";
		public override EnumUtility.AttackType Element => EnumUtility.AttackType.Basilisk;
		public override string Rarity => "Common";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.BlindingCloud;
		public override int Charges => 5;


		private GameObject player;

		public BlindingCloud(Transform container) : base(container) { }

		protected override void Awaker() {
			base.Awaker();
			bookIcon = Resources.Load<Sprite>("Images/BasiliskCommonBook");
		}


		public override void UseConsumable() {

			RemoveCharge();     // Remove charge after the ability is used
		}
	}
}