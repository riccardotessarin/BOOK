using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Characters.Interfaces;
using Attacks;


namespace Consumables.Books.Abilities {
	class FirePillarBehavior : MonoBehaviour {

		private void Awake() {
		}

		// Use this for initialization
		void Start() {

		}

		/*
		private void OnTriggerEnter(Collider other) {
			Transform child = gameObject.transform.GetChild(0);
			ParticleSystem particle = child.GetComponent<ParticleSystem>();
			if (!particle.isPlaying) {
				particle.Play();
			}
		}

		*/
		private void OnCollisionEnter(Collision collision) {
			//gameObject.GetComponentInChildren<Renderer>().enabled = true;
			//gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = true;
			Character enemy = collision.gameObject.GetComponent<Character>();
			if (enemy != null) {
				Transform child = gameObject.transform.GetChild(0);
				ParticleSystem particle = child.GetComponent<ParticleSystem>();
				if (!particle.isPlaying) {
					particle.Play();
				}
			}

			//ParticleSystem ps = gameObject.GetComponentInChildren<ParticleSystem>();
			//ps.enableEmission = true;
			/*
			Character enemy = collision.gameObject.GetComponent<NonPlayableCharacters>();
			if (enemy != null) {
				gameObject.GetComponentInChildren<Renderer>().enabled = true;

				Character.Damage firePillarDamage = new Character.Damage(200.0F , AttackType.Inferno);
				Debug.Log("NPC hitted");
				enemy.SendMessage("TakeDamage", firePillarDamage, SendMessageOptions.DontRequireReceiver);

				Destroy(gameObject);
			}
			*/
		}

		// Update is called once per frame
		void Update() {

		}
	}
}
