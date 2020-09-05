using System;

namespace Consumables.Pages.Abilities {
	class LightningSpeedPage : Page {
		public override string Name => "Lightning Speed Page";

		public override string Description => "Adds a charge for Lightning Speed BOOK";

		public override string Rarity => "Common";

		public override EnumUtility.PageType Type => EnumUtility.PageType.LightningSpeed;

		public override void UseConsumable() {
			throw new NotImplementedException();
		}
	}
}
