using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Characters.Interfaces;
using Attacks;
using System.Collections;

namespace Consumables.Books.Abilities {
	class FirePillarBehavior : MonoBehaviour {

		private void Awake() {
		}

		// Use this for initialization
		void Start() {

		}

		private void OnCollisionEnter(Collision collision) {
			Character enemy = collision.gameObject.GetComponent<Character>();
			if (enemy != null) {
				Transform child = gameObject.transform.GetChild(0);
				ParticleSystem particle = child.GetComponent<ParticleSystem>();
				if (!particle.isPlaying) {
					particle.Play();
				}
				Character.Damage firePillarDamage = new Character.Damage(200.0F, EnumUtility.AttackType.Inferno);
				Debug.Log(enemy + " hitted");
				enemy.SendMessage("TakeDamage", firePillarDamage, SendMessageOptions.DontRequireReceiver);
				StartCoroutine(WaitAndDestroy(30.0F));
			}
		}

		private IEnumerator WaitAndDestroy(float waitTime) {
			while (true) {
				yield return new WaitForSecondsRealtime(waitTime);
				Destroy(gameObject);
			}
		}

		// Update is called once per frame
		void Update() {

		}
	}
}
