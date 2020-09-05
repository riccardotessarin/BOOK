using System;

namespace Consumables.Pages.Abilities {
	class SurgingTidePage : Page {
		public override string Name => "Surging Tide Page";

		public override string Description => "Adds a charge for Surging Tide BOOK";

		public override string Rarity => "Rare";

		public override EnumUtility.PageType Type => EnumUtility.PageType.SurgingTide;

		public override void UseConsumable() {
			throw new NotImplementedException();
		}
	}
}
