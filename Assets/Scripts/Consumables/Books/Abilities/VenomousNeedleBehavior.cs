using UnityEngine;
using System.Collections;
using Characters.Interfaces;

public class VenomousNeedleBehavior : MonoBehaviour {
	private void Awake() {
		StartCoroutine(WaitAndDestroy(30.0F));
	}

	// This function destroys the fireball after a certain time if it doesn't hit anything
	private IEnumerator WaitAndDestroy(float waitTime) {
		while (true) {
			yield return new WaitForSecondsRealtime(waitTime);
			Destroy(gameObject);
		}
	}

	private void OnCollisionEnter(Collision collision) {
		Character enemy = collision.gameObject.GetComponent<Character>();
		if (enemy != null) {
			Debug.Log(enemy + " hitted");
			StartCoroutine(DamageOverTime(enemy));
		} else {
			Destroy(gameObject);
		}
	}

	// Incomplete
	private IEnumerator DamageOverTime(Character enemy) {
		Character.Damage needleDamage = new Character.Damage(3.0F /*damage dealt*/, EnumUtility.AttackType.Basilisk);
		float totalDamage = 15.0F;
		while (totalDamage > 0) {
			enemy.SendMessage("TakeDamage", needleDamage, SendMessageOptions.DontRequireReceiver);
			yield return new WaitForSecondsRealtime(3);
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}
}
