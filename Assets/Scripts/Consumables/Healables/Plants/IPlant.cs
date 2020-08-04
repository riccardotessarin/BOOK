using UnityEngine;
using System.Collections;

public interface IPlant : IHealable {
	enum PlantType { Genea, Venomyaz, Yukisnow }

	PlantType Type { get; }

	/// <summary>
	/// Trigger a random malus.
	/// </summary>
	/// <returns></returns>
	void TriggerMalus();

	/// <summary>
	/// Check if the current race and current plant are compatible.
	/// </summary>
	/// <returns></returns>
	void CheckCompatibility();
}
