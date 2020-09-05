using System;

namespace Consumables.Pages.Abilities {
	class BlindingCloudPage : Page {
		public override string Name => "Blinding Cloud Page";

		public override string Description => "Adds a charge for Blinding Cloud BOOK";

		public override string Rarity => "Common";

		public override EnumUtility.PageType Type => EnumUtility.PageType.BlindingCloud;

		public override void UseConsumable() {
			throw new NotImplementedException();
		}
	}
}