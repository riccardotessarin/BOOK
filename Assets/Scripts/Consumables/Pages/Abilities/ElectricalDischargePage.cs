using System;

namespace Consumables.Pages.Abilities {
	class ElectricalDischargePage : Page {
		public override string Name => "Electrical Discharge Page";

		public override string Description => "Adds a charge for Electrical Discharge BOOK";

		public override string Rarity => "Rare";

		public override EnumUtility.PageType Type => EnumUtility.PageType.ElectricalDischarge;

		public override void UseConsumable() {
			throw new NotImplementedException();
		}
	}
}