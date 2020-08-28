using UnityEngine;
using System.Collections;

namespace Consumables.Books.Abilities {
	public class ElectricalDischarge : Book {

		public override string Name => "Electrical Discharge";
		public override string Description => "Evoke an electric discharge which links to nearby enemies, stunning them and dealing damage.";
		public override EnumUtility.AttackType Element => EnumUtility.AttackType.Raijin;
		public override string Rarity => "Rare";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.ElectricalDischarge;
		public override int Charges => 3;


		private GameObject player;

		public ElectricalDischarge(Transform container) : base(container) { }

		protected override void Awaker() {
			base.Awaker();
			bookIcon = Resources.Load<Sprite>("Images/RaijinRareBook");
		}


		public override void UseConsumable() {

			RemoveCharge();     // Remove charge after the ability is used
		}
	}
}