using UnityEngine;
using System.Collections;
using Characters;
using Characters.Interfaces;

public class FireballBehavior : MonoBehaviour {

	[SerializeField] private GameObject explosionVFX;

	// Use this for initialization
	void Start() {

	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.GetComponent<NonPlayableCharacters>() != null) {
			Debug.Log("NPC hitted");
			Instantiate(explosionVFX, transform.parent);
			Destroy(gameObject);
		}

		/*
		enemy = collision.gameObject.GetComponent<NPC>();
		if (enemy != null) {
			Debug.Log("NPC hitted");
			Destroy(gameObject);
			DealDamage(100, enemy);
			Instantiate(explosionVFX, transform.parent);
		}
		
		 */
	}

	/*
	private void DealDamage(int damage, type? enemy) {
		enemy.currentHealth -= damage;
	}
	*/

	// Update is called once per frame
	void Update() {

	}
}
