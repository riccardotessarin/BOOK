using UnityEngine;
using System.Collections;
using System.Linq;
using Managers;
using Photon.Pun;
using Characters.Interfaces;

namespace Consumables.Books.Abilities {
	public class VenomousNeedle : Book {

		public override string Name => "Venomous Needle";
		public override string Description => "Shoot a toxic needle which deals damage over time.";
		public override EnumUtility.AttackType Element => EnumUtility.AttackType.Basilisk;
		public override string Rarity => "Rare";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.VenomousNeedle;
		public override int Charges => 3;

		[SerializeField] private float needleSpeed = 5F;
		private GameObject venomousNeedlePrefab;
		private GameObject player;

		public VenomousNeedle(Transform container) : base(container) { }

		protected override void Awaker() {
			base.Awaker();
			bookIcon = Resources.Load<Sprite>("Images/BasiliskRareBook");
			venomousNeedlePrefab = Resources.Load("Prefabs/Attacks/VenomousNeedle") as GameObject;
		}

		/*
		public override void UseConsumable() {
			var players = GameObject.FindGameObjectsWithTag("Player");
			//player = players.FirstOrDefault(player => player.GetComponent<PhotonView>().IsMine);
			player = players.FirstOrDefault();
			var playerTransform = player.transform;
			// Define the behavior of the ability
			//bookVFX = Object.Instantiate(fireballPrefab, playerTransform.position + playerTransform.forward * 2, playerTransform.rotation);
			Camera camera = Camera.main;
			bookVFX = Object.Instantiate(venomousNeedlePrefab, camera.transform.position + camera.transform.forward * 2, camera.transform.rotation);
			bookVFX.transform.parent = container;
			GameManager.Instance.StartCoroutine(MoveNeedle());
			RemoveCharge();     // Remove charge after the ability is used
		}
		*/

		public override void UseConsumable() {
			var players = GameObject.FindGameObjectsWithTag("Player");
			player = players.FirstOrDefault(p => p.GetComponent<PhotonView>().IsMine);
			if (Equals(player, null)) return;

			var cameraTransform = player.GetComponent<PlayableCharacter>().Camera.transform;
			bookVFX = PhotonNetwork.Instantiate("Prefabs/Attacks/VenomousNeedle", cameraTransform.position + cameraTransform.forward * 2, cameraTransform.rotation);
			bookVFX.transform.parent = container;

			GameManager.Instance.StartCoroutine(MoveNeedle());
			// player.GetComponent<PhotonView>().RPC("RPC_ShootFireball", RpcTarget.All, cameraTransform.position, cameraTransform.rotation, cameraTransform.forward);

			RemoveCharge(); // Remove charge after the ability is used
		}

		private IEnumerator MoveNeedle() {
			while (true) {
				yield return new WaitForEndOfFrame();
				if (bookVFX != null) {
					bookVFX.transform.Translate(Vector3.forward * Time.unscaledDeltaTime * needleSpeed, Space.Self);
				}
			}
		}
	}
}