using UnityEngine;
using System.Collections;
using Characters.Interfaces;

namespace Consumables.Healables.Plants {
	public interface IPlant : IHealable {

		EnumUtility.CharacterType Type { get; } // Used charactertype to store which race is compatible

		/// <summary>
		/// Trigger a random malus when a non-compatible plant is eaten.
		/// </summary>
		/// <returns></returns>
		void TriggerMalus(PlayableCharacter currentPlayer);

		/// <summary>
		/// Check if the current race and current plant are compatible.
		/// </summary>
		/// <returns> True if plant is compatible </returns>
		bool CheckCompatibility(PlayableCharacter currentPlayer);
	}
}