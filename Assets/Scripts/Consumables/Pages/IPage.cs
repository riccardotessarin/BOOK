using UnityEngine;
using System.Collections;

namespace Consumables.Pages {
	public interface IPage : IConsumable {


		string Rarity { get; }
		EnumUtility.PageType Type { get; }
	}
}
