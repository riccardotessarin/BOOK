using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Characters.Interfaces;
using Attacks;
using System.Collections;
using Photon.Pun;

namespace Consumables.Books.Abilities {
	class FirePillarBehavior : MonoBehaviour {

		private bool active = false;

		private void Awake() {
		}

		// Use this for initialization
		void Start() {

		}

		private void OnTriggerEnter(Collider other) {
			Character enemy = other.gameObject.GetComponent<Character>();
			if (enemy != null && !active) {
				active = true;
				Transform child = gameObject.transform.GetChild(0);
				ParticleSystem particle = child.GetComponent<ParticleSystem>();
				if (!particle.isPlaying) {
					particle.Play();
				}
				Character.Damage firePillarDamage = new Character.Damage(20F, EnumUtility.AttackType.Inferno);
				Debug.Log(enemy + " hitted");
				enemy.SendMessage("TakeDamage", firePillarDamage, SendMessageOptions.DontRequireReceiver);
				StartCoroutine(WaitAndDestroy(30.0F));
			}
		}

		private void OnCollisionEnter(Collision collision) {
			Character enemy = collision.gameObject.GetComponent<Character>();
			if (enemy != null) {
				Transform child = gameObject.transform.GetChild(0);
				ParticleSystem particle = child.GetComponent<ParticleSystem>();
				if (!particle.isPlaying) {
					particle.Play();
				}
				Character.Damage firePillarDamage = new Character.Damage(10.0F, EnumUtility.AttackType.Inferno);
				Debug.Log(enemy + " hitted");
				enemy.SendMessage("TakeDamage", firePillarDamage, SendMessageOptions.DontRequireReceiver);
				StartCoroutine(WaitAndDestroy(30.0F));
			}
		}

		private IEnumerator WaitAndDestroy(float waitTime) {
			while (true) {
				yield return new WaitForSecondsRealtime(waitTime);
				PhotonNetwork.Destroy(gameObject);
			}
		}

		// Update is called once per frame
		void Update() {

		}
	}
}
