﻿using UnityEngine;
using System.Collections;
using System.Linq;
using Managers;
using Photon.Pun;

namespace Consumables.Books.Abilities {
	public class WaterShield : Book {

		public override string Name => "Water Shield";
		public override string Description => "Create a water shield that protects against attacks.";
		public override EnumUtility.AttackType Element => EnumUtility.AttackType.Neptunian;
		public override string Rarity => "Common";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.WaterShield;
		public override int Charges => 5;

		[SerializeField] private GameObject waterShieldPrefab;

		private GameObject player;

		public WaterShield(Transform container) : base(container) { }

		protected override void Awaker() {
			base.Awaker();
			bookIcon = Resources.Load<Sprite>("Images/NeptunianCommonBook");
			waterShieldPrefab = Resources.Load("Prefabs/Attacks/WaterShield") as GameObject;
		}


		public override void UseConsumable() {
			var players = GameObject.FindGameObjectsWithTag("Player");
			//player = players.FirstOrDefault(player => player.GetComponent<PhotonView>().IsMine);
			player = players.FirstOrDefault();
			var playerTransform = player.transform;
			// Define the behavior of the ability
			bookVFX = PhotonNetwork.Instantiate("Prefabs/Attacks/WaterShield", playerTransform.position + playerTransform.forward * 2 + playerTransform.up, playerTransform.rotation);
			//Camera camera = Camera.main;
			//bookVFX = Object.Instantiate(fireballPrefab, camera.transform.position + camera.transform.forward * 2, camera.transform.rotation);
			bookVFX.transform.parent = container;
			RemoveCharge();     // Remove charge after the ability is used
		}
	}
}