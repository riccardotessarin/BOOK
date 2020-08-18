using UnityEngine;
using System.Collections;
using MalusEBonus;
using User;
using Characters.Interfaces;
using UnityEngine.Playables;

namespace Consumables.Healables.Plants {
	public abstract class Plant : MonoBehaviour, IPlant {
		public abstract string Name { get; }
		public abstract string Description { get; }
		public int HealthPercentage => 50;
		public abstract EnumUtility.CharacterType Type { get; }


		public bool CheckCompatibility() {
			Character character = gameObject.AddComponent<Character>();
			if (Type.ToString() == character.type) {
				return true;
			}

			return false;
		}

		public void TriggerMalus() {
			System.Random randomizer = new System.Random();
			int malusChoice = randomizer.Next(3);
			Bonus malus;
			PlayableCharacter playableCharacter = GameObject.FindWithTag("player").GetComponent<PlayableCharacter>();
			MalusManager malusManager = playableCharacter.malusManager;

			switch (malusChoice) {
				case 0:
					malus = new Bonus(false, MalusManager.Stats.Hp, 0.7f, "plantHealthMalus");
					malusManager.Add(malus);
					StartCoroutine(WaitAndRemoveMalus(30.0F, malus, malusManager));
					//malusManager.Remove(MalusManager.Stats.Hp, "plantHealthMalus");
					break;
				case 1:
					malus = new Bonus(false, MalusManager.Stats.Stamina, 0.7f, "plantStaminaMalus");
					malusManager.Add(malus);
					StartCoroutine(WaitAndRemoveMalus(30.0F, malus, malusManager));
					break;
				case 2:
					malus = new Bonus(false, MalusManager.Stats.Speed, 0.7f, "plantSpeedMalus");
					malusManager.Add(malus);
					StartCoroutine(WaitAndRemoveMalus(30.0F, malus, malusManager));
					break;
			}

		}

		private IEnumerator WaitAndRemoveMalus(float waitTime, Bonus malus, MalusManager malusManager) {
			yield return new WaitForSecondsRealtime(waitTime);
			malusManager.Remove(malus.Stat, malus.Name);
		}

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