using UnityEngine;
using System.Collections;

public interface IConsumable {
	string Name { get; }
	string Description { get; }

	/// <summary>
	/// Define the behavior of the consumable.
	/// It activates the BOOK ability or restores health used with plants
	/// </summary>
	/// <returns>
	/// Returns the damage dealt or health restored
	/// </returns>
	int UseConsumable();

	string ToString();
}
