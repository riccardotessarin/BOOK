using System;

namespace Consumables.Pages.Abilities {
	class VenomousNeedlePage : Page {
		public override string Name => "Venomous Needle Page";

		public override string Description => "Adds a charge for Venomous Needle BOOK";

		public override string Rarity => "Rare";

		public override EnumUtility.PageType Type => EnumUtility.PageType.VenomousNeedle;

		public override void UseConsumable() {
			throw new NotImplementedException();
		}
	}
}
