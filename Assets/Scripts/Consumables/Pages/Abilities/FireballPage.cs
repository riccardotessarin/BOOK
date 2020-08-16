using Consumables.Books.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumables.Pages.Abilities {
	class FireballPage : Page {
		public override string Name => "Fireball Page";

		public override string Description => "Adds a charge for Fireball BOOK";

		public override string Rarity => "Rare";

		public override EnumUtility.PageType Type => EnumUtility.PageType.Fireball;

		public override void UseConsumable() {
			throw new NotImplementedException();
		}
	}
}
