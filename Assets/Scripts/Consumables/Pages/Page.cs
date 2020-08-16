using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumables.Pages {
	public abstract class Page : IPage {
		public abstract string Name { get; }
		public abstract string Description { get; }
		public abstract string Rarity { get; }

		public abstract EnumUtility.PageType Type { get; }

		//Use consumables probably does nothing for pages
		public abstract void UseConsumable();
	}
}
