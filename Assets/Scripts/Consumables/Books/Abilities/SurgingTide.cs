using UnityEngine;
using System.Collections;

namespace Consumables.Books.Abilities {
	public class SurgingTide : Book {

		public override string Name => "Surging Tide";
		public override string Description => "Evoke a tide that drives all enemies away, without dealing damage.";
		public override EnumUtility.AttackType Element => EnumUtility.AttackType.Neptunian;
		public override string Rarity => "Rare";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.SurgingTide;
		public override int Charges => 3;


		private GameObject player;

		public SurgingTide(Transform container) : base(container) { }

		protected override void Awaker() {
			base.Awaker();
			bookIcon = Resources.Load<Sprite>("Images/NeptunianRareBook");
		}


		public override void UseConsumable() {

			RemoveCharge();     // Remove charge after the ability is used
		}
	}
}