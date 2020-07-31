using UnityEngine;
using System.Collections;

public interface IPage : IConsumable {
	enum PageType { Fireball }
	
	string Rarity { get; }
	PageType Type { get; }
}
