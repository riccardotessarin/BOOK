using UnityEngine;
using System.Collections;
using System.Linq;
using Characters.Interfaces;

namespace Consumables.Books.Abilities {
	public class LightningSpeedBehavior : MonoBehaviour {

		private GameObject player;

		private void Awake() {
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
			// Finding player
			var players = GameObject.FindGameObjectsWithTag("Player");
			//player = players.FirstOrDefault(player => player.GetComponent<PhotonView>().IsMine);
			player = players.FirstOrDefault();
		}

		// Update is called once per frame
		void Update() {
			gameObject.transform.position = player.transform.position;
		}
	}
}