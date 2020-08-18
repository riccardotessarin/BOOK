using UnityEngine;
using System.Collections;
using MalusEBonus;
using User;
using Characters.Interfaces;

namespace Consumables.Healables.Plants {
	public abstract class Plant : MonoBehaviour, IPlant {
		public abstract string Name { get; }
		public abstract string Description { get; }
		public int HealthPercentage => 50;
		public abstract EnumUtility.CharacterType Type { get; }


		public bool CheckCompatibility() {
			bool compatible = false;
			if (this.Type == /*Character.Type*/) {
				compatible = true;
			}

			return compatible;
		}

		public void TriggerMalus() {
			System.Random randomizer = new System.Random();
			int malusChoice = randomizer.Next(3);
			Bonus malus;
			MalusManager malusManager = new MalusManager();

			//NOTE: It may be more efficient to add a malus "lifetime" timer into Bonus class
			switch (malusChoice) {
				case 0:
					malus = new Bonus(false, MalusManager.Stats.Hp, 0.7f, "plantHealthMalus");
					malusManager.Add(malus);
					//TODO: Wait for time T
					malusManager.Remove(MalusManager.Stats.Hp, "plantHealthMalus");
					break;
				case 1:
					malus = new Bonus(false, MalusManager.Stats.Stamina, 0.7f, "plantStaminaMalus");
					malusManager.Add(malus);
					//TODO: Wait for time T
					malusManager.Remove(MalusManager.Stats.Stamina, "plantStaminaMalus");
					break;
				case 2:
					malus = new Bonus(false, MalusManager.Stats.Speed, 0.7f, "plantSpeedMalus");
					malusManager.Add(malus);
					//TODO: Wait for time T
					malusManager.Remove(MalusManager.Stats.Speed, "plantSpeedMalus");
					break;
			}

		}

		/*
		1 - Check Compatibility and store bool in a variable
		2 - Give health to player
		3 - If compatibility is false trigger malus
		4 - Remove the plant from inventory
		*/
		public void UseConsumable() {
			bool compatible = CheckCompatibility();
			//TODO: Trigger a restore health function
			Inventory.Instance.TryRemoveConsumableFromInventory(this);
			Destroy(this);
			if (!compatible) {
				TriggerMalus();
			}
		}

		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}
	}
}