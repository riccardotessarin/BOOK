using System;

namespace Consumables.Pages.Abilities {
	class FirePillarPage : Page {
		public override string Name => "Fire Pillar Page";

		public override string Description => "Adds a charge for Fire Pillar BOOK";

		public override string Rarity => "Common";

		public override EnumUtility.PageType Type => EnumUtility.PageType.FirePillar;

		public override void UseConsumable() {
			throw new NotImplementedException();
		}
	}
}