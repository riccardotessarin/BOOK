using System;

namespace Consumables.Pages.Abilities {
	class IceStalagmitePage : Page {
		public override string Name => "Ice Stalagmite Page";

		public override string Description => "Adds a charge for Ice Stalagmite BOOK";

		public override string Rarity => "Common";

		public override EnumUtility.PageType Type => EnumUtility.PageType.IceStalagmite;

		public override void UseConsumable() {
			throw new NotImplementedException();
		}
	}
}

