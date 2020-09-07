using UnityEngine;
using System.Collections;
using MalusEBonus;
using User;
using Characters.Interfaces;
using UnityEngine.Playables;
using System;
using Managers;
using System.Linq;
using Photon.Pun;

namespace Consumables.Healables.Plants {
	public abstract class Plant : IPlant {
		public abstract string Name { get; }
		public abstract string Description { get; }
		public float HealthPercentage => 50;
		public abstract EnumUtility.CharacterType Type { get; }

		[SerializeField] protected Sprite plantIcon;
		public Sprite PlantIcon { get => plantIcon; }

		protected Transform container;

		public Plant(Transform container) {
			this.container = container;
			Awaker();
		}

		protected virtual void Awaker() {	}

		public void UseConsumable() {
			var players = GameObject.FindGameObjectsWithTag("Player");
			var player = players.FirstOrDefault(p => p.GetComponent<PhotonView>().IsMine);
			if (Equals(player, null)) return;
			Character toHeal = player.GetComponent<Character>();
			PlayableCharacter currentPlayer = player.GetComponent<PlayableCharacter>();
			bool compatible = CheckCompatibility(currentPlayer);

			GameManager.Instance.StartCoroutine(HealOverTime(1.0f, toHeal));

			Inventory.Instance.TryRemoveConsumableFromInventory(this);
			if (!compatible) {
				TriggerMalus(currentPlayer);
			}
			//Destroy(gameObject);
		}

		public bool CheckCompatibility(PlayableCharacter currentPlayer) {
			if (Type == currentPlayer.RaceType) {
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
					GameManager.Instance.StartCoroutine(WaitAndRemoveMalus(30.0F, malus, malusManager));
					//malusManager.Remove(MalusManager.Stats.Hp, "plantHealthMalus");
					break;
				case 1:
					malusName = "plantStaminaMalus" + DateTime.Now.ToString("s");
					malus = new Bonus(false, MalusManager.Stats.Stamina, 0.7f, malusName);
					malusManager.Add(malus);
					GameManager.Instance.StartCoroutine(WaitAndRemoveMalus(30.0F, malus, malusManager));
					break;
				case 2:
					malusName = "plantSpeedMalus" + DateTime.Now.ToString("s");
					malus = new Bonus(false, MalusManager.Stats.Speed, 0.7f, malusName);
					malusManager.Add(malus);
					GameManager.Instance.StartCoroutine(WaitAndRemoveMalus(30.0F, malus, malusManager));
					break;
			}

		}

		private IEnumerator HealOverTime(float waitTime, Character toHeal) {
			float healed = 0.0f;
			float healDot = 2.0f;
			while(healed < HealthPercentage) {
				yield return new WaitForSecondsRealtime(waitTime);
				toHeal.SendMessage("RecoverHP", healDot, SendMessageOptions.DontRequireReceiver);
				healed += healDot;
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