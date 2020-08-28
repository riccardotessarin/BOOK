using UnityEngine;
using System.Collections;

namespace Consumables.Books.Abilities {
	public class LightningSpeed : Book {

		public override string Name => "Lightning Speed";
		public override string Description => "Send an electrical discharge through your body that grants a temporary increase in speed.";
		public override EnumUtility.AttackType Element => EnumUtility.AttackType.Raijin;
		public override string Rarity => "Common";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.LightningSpeed;
		public override int Charges => 3;


		private GameObject player;

		public LightningSpeed(Transform container) : base(container) { }

		protected override void Awaker() {
			base.Awaker();
			bookIcon = Resources.Load<Sprite>("Images/RaijinCommonBook");
		}


		public override void UseConsumable() {

			RemoveCharge();     // Remove charge after the ability is used
		}
	}
}