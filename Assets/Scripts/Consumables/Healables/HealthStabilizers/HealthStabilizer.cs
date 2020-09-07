using Characters.Interfaces;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Consumables.Healables.HealthStabilizers {
	public class HealthStabilizer : MonoBehaviour, IHealable {
		[SerializeField] private GameObject healthStabilizer3DModel;

		public string Name => "Health Stabilizer";

		public string Description => "A pill made of condensed blood. You can use it to instantly restore 10% of your health.";

		public float HealthPercentage => 10;

		public void UseConsumable() {
			var players = GameObject.FindGameObjectsWithTag("Player");
			var player = players.FirstOrDefault(p => p.GetComponent<PhotonView>().IsMine);
			if (Equals(player, null)) return;
			var toHeal = player.GetComponent<Character>();
			toHeal.SendMessage("RecoverHP", HealthPercentage, SendMessageOptions.DontRequireReceiver);
			Destroy(this.gameObject);
		}

		#region UnityMethods

		private void Awake() {
			healthStabilizer3DModel = Resources.Load<GameObject>("");
		}

		private void Start() {
			
		}

		private void Update() {
			
		}
		#endregion
	}
}
