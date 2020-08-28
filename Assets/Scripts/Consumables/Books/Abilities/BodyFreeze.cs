using UnityEngine;
using System.Collections;

namespace Consumables.Books.Abilities {
	public class BodyFreeze : Book {

		public override string Name => "Body Freeze";
		public override string Description => "Freeze the body of a specific target.";
		public override EnumUtility.AttackType Element => EnumUtility.AttackType.Niflheim;
		public override string Rarity => "Rare";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.BodyFreeze;
		public override int Charges => 3;


		private GameObject player;

		public BodyFreeze(Transform container) : base(container) { }

		protected override void Awaker() {
			base.Awaker();
			bookIcon = Resources.Load<Sprite>("Images/NifhleimRareBook");
		}


		public override void UseConsumable() {

			RemoveCharge();     // Remove charge after the ability is used
		}
	}
}
