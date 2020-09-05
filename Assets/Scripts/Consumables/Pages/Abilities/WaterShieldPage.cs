using System;

namespace Consumables.Pages.Abilities {
	class WaterShieldPage : Page {
		public override string Name => "Water Shield Page";

		public override string Description => "Adds a charge for Water Shield BOOK";

		public override string Rarity => "Common";

		public override EnumUtility.PageType Type => EnumUtility.PageType.WaterShield;

		public override void UseConsumable() {
			throw new NotImplementedException();
		}
	}
}
