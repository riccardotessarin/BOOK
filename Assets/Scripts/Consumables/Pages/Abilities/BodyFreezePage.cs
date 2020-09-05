using System;

namespace Consumables.Pages.Abilities {
	class BodyFreezePage : Page {
		public override string Name => "Body Freeze Page";

		public override string Description => "Adds a charge for Body Freeze BOOK";

		public override string Rarity => "Rare";

		public override EnumUtility.PageType Type => EnumUtility.PageType.BodyFreeze;

		public override void UseConsumable() {
			throw new NotImplementedException();
		}
	}
}
