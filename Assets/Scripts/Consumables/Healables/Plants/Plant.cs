using UnityEngine;
using System.Collections;

namespace Consumables.Healables.Plants {
	public abstract class Plant : MonoBehaviour, IPlant {
		public abstract string Name { get; }
		public abstract string Description { get; }
		public int HealthPercentage => 50;
		public abstract EnumUtility.PlantType Type { get; }

		/*
		Check if current race and current plant are compatible
		*/
		public abstract void CheckCompatibility();

		/*
		Random malus trigger
		*/
		public abstract void TriggerMalus();

		/*
		1 - Check Compatibility and store bool in a variable
		2 - Give health to player
		3 - If compatibility is false trigger malus
		4 - Remove the plant from inventory
		5 - Return amount of health restored
		*/
		public abstract int UseConsumable();

		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}
	}
}