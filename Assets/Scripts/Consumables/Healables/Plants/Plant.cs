using UnityEngine;
using System.Collections;
using MalusEBonus;
using User;
using Characters.Interfaces;
using UnityEngine.Playables;
using System;

namespace Consumables.Healables.Plants {
	public abstract class Plant : MonoBehaviour, IPlant {
		public abstract string Name { get; }
		public abstract string Description { get; }
		public int HealthPercentage => 50;
		public abstract EnumUtility.CharacterType Type { get; }

		[SerializeField] protected Sprite plantIcon;
		public Sprite PlantIcon { get => plantIcon; }

		public void UseConsumable() {
			PlayableCharacter currentPlayer = GameObject.FindWithTag("Player").GetComponent<PlayableCharacter>();
			bool compatible = CheckCompatibility(currentPlayer);
			//TODO: Trigger a restore health function
			Inventory.Instance.TryRemoveConsumableFromInventory(this);
			Destroy(this);
			if (!compatible) {
				TriggerMalus(currentPlayer);
			}
		}

		public bool CheckCompatibility(PlayableCharacter currentPlayer) {
			if (Type.ToString() == currentPlayer.type) {
				return true;
			}

			return false;
		}

		public void TriggerMalus(PlayableCharacter currentPlayer) {
			System.Random randomizer = new System.Random();
			int malusChoice = randomizer.Next(3);
			Bonus malus;
			MalusManager malusManager = currentPlayer.malusManager;
			string malusName;

			switch (malusChoice) {
				case 0:
					malusName = "plantHealthMalus" + DateTime.Now.ToString("s");
					malus = new Bonus(false, MalusManager.Stats.Hp, 0.7f, malusName);
					malusManager.Add(malus);
					StartCoroutine(WaitAndRemoveMalus(30.0F, malus, malusManager));
					//malusManager.Remove(MalusManager.Stats.Hp, "plantHealthMalus");
					break;
				case 1:
					malusName = "plantStaminaMalus" + DateTime.Now.ToString("s");
					malus = new Bonus(false, MalusManager.Stats.Stamina, 0.7f, malusName);
					malusManager.Add(malus);
					StartCoroutine(WaitAndRemoveMalus(30.0F, malus, malusManager));
					break;
				case 2:
					malusName = "plantSpeedMalus" + DateTime.Now.ToString("s");
					malus = new Bonus(false, MalusManager.Stats.Speed, 0.7f, malusName);
					malusManager.Add(malus);
					StartCoroutine(WaitAndRemoveMalus(30.0F, malus, malusManager));
					break;
			}

		}

		private IEnumerator WaitAndRemoveMalus(float waitTime, Bonus malus, MalusManager malusManager) {
			yield return new WaitForSecondsRealtime(waitTime);
			malusManager.Remove(malus.Stat, malus.Name);
		}

		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}
	}
}