using UnityEngine;
using System.Collections;
using MalusEBonus;
using Characters.Interfaces;
using Managers;
using System.Linq;
using Photon.Pun;

namespace Consumables.Books.Abilities {
	public class LightningSpeed : Book {

		public override string Name => "Lightning Speed";
		public override string Description => "Send an electrical discharge through your body that grants a temporary increase in speed.";
		public override EnumUtility.AttackType Element => EnumUtility.AttackType.Raijin;
		public override string Rarity => "Common";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.LightningSpeed;
		public override int Charges => 3;

		private GameObject electricityPrefab;
		private GameObject player;

		public LightningSpeed(Transform container) : base(container) { }

		protected override void Awaker() {
			base.Awaker();
			bookIcon = Resources.Load<Sprite>("Images/RaijinCommonBook");
			electricityPrefab = Resources.Load("Prefabs/Attacks/Electricity") as GameObject;
		}


		public override void UseConsumable() {
			// Finding player
			var players = GameObject.FindGameObjectsWithTag("Player");
			//player = players.FirstOrDefault(player => player.GetComponent<PhotonView>().IsMine);
			player = players.FirstOrDefault();
			var playerTransform = player.transform;
			PlayableCharacter currentPlayer = player.GetComponent<PlayableCharacter>();
			
			// Applying speed bonus
			MalusManager malusManager = currentPlayer.malusManager;
			var bonusName = "lightningSpeedBookBonus" + System.DateTime.Now.ToString("s");
			var bonus = new Bonus(true, MalusManager.Stats.Speed, 0.7f, bonusName);
			malusManager.Add(bonus);

			// Instance prefab
			bookVFX = PhotonNetwork.Instantiate("Prefabs/Attacks/Electricity", playerTransform.position, playerTransform.rotation);

			// Wait and destroy
			GameManager.Instance.StartCoroutine(WaitAndRemoveBookBonus(30.0F, bonus, malusManager));
			RemoveCharge();     // Remove charge after the ability is used
		}

		private IEnumerator WaitAndRemoveBookBonus(float waitTime, Bonus bonus, MalusManager malusManager) {
			yield return new WaitForSecondsRealtime(waitTime);
			malusManager.Remove(bonus.Stat, bonus.Name);
		}
	}
}