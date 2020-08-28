using UnityEngine;
using System.Collections;
using Characters;
using Characters.Interfaces;
using Attacks;

public class FireballBehavior : MonoBehaviour {

	[SerializeField] private GameObject explosionVFX;

	private void Awake() {
		explosionVFX = Resources.Load("") as GameObject;
	}

	// Use this for initialization
	void Start() {

	}

	private void OnCollisionEnter(Collision collision) {
		Character enemy = collision.gameObject.GetComponent<Character>();
		Character.Damage fireballDamage = new Character.Damage(600.0F /*damage dealt*/, EnumUtility.AttackType.Inferno);
		if (enemy != null) {
			Debug.Log("NPC hitted");
			enemy.SendMessage("TakeDamage", fireballDamage, SendMessageOptions.DontRequireReceiver);
		}
		Instantiate(explosionVFX, transform.parent);
		Destroy(gameObject);
	}

	// Update is called once per frame
	void Update() {

	}
}
