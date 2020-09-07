using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Characters.Interfaces;
using Photon.Pun;
using UnityEngine;
namespace Consumables.Books.Abilities{
	public class IceStalagmite : Book
	{
		public override string Name => "Ice Stalagmite";
		public override string Description => "Set a trap that create a stalagmite of ice";
		public override EnumUtility.AttackType Element => EnumUtility.AttackType.Niflheim;
		public override string Rarity => "Common";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.IceStalagmite;
		public override int Charges => 3;

		[SerializeField] private GameObject iceStalagmitePrefab;
		private GameObject player;


		public IceStalagmite(Transform container) : base(container) {	}

		protected override void Awaker(){
			base.Awaker();
			bookIcon=Resources.Load<Sprite>("Images/NifhleimCommonBook");
			iceStalagmitePrefab = Resources.Load("Prefabs/Attacks/IceStalagmite") as GameObject;
		}

		public override void UseConsumable() {
			var players = GameObject.FindGameObjectsWithTag("Player");
			player = players.FirstOrDefault(p => p.GetComponent<PhotonView>().IsMine);
			if (Equals(player, null)) return;
			var playerTransform = player.transform;

			var cameraTransform = player.GetComponent<PlayableCharacter>().Camera.transform;
			RaycastHit hit;
			Physics.Raycast(playerTransform.position, cameraTransform.forward, out hit, 6);
			bookVFX = PhotonNetwork.Instantiate("Prefabs/Attacks/IceStalagmite", hit.point, playerTransform.rotation);
			bookVFX.transform.parent = container;
			RemoveCharge();     // Remove charge after the ability is used
		}
	}
}
