using UnityEngine;
using System.Collections;
using System.Linq;
using Characters.Interfaces;
using Photon.Pun;

namespace Consumables.Books.Abilities {
	public class LightningSpeedBehavior : MonoBehaviour {

		private GameObject player;

		private void Awake() {
			var players = GameObject.FindGameObjectsWithTag("Player");
			player = players.FirstOrDefault(p => p.GetComponent<PhotonView>().IsMine);
			if (Equals(player, null)) return;
			StartCoroutine(WaitAndDestroy(30.0F));
		}

		private IEnumerator WaitAndDestroy(float waitTime) {
			while (true) {
				yield return new WaitForSecondsRealtime(waitTime);
				Destroy(gameObject);
			}
		}

		// Use this for initialization
		void Start() {
		}

		// Update is called once per frame
		void Update() {
			gameObject.transform.position = player.transform.position;
		}
	}
}