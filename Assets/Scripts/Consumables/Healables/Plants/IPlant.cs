using UnityEngine;
using System.Collections;

namespace Consumables.Healables.Plants {
	public interface IPlant : IHealable {

		EnumUtility.PlantType Type { get; }

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
}