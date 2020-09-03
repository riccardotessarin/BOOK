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
			Character.Damage needleDamage = new Character.Damage(10.0F /*damage dealt*/, EnumUtility.AttackType.Basilisk);
			enemy.SendMessage("TakeDamage", needleDamage, SendMessageOptions.DontRequireReceiver);
			enemy.Poisoned = true;
		}
		Destroy(gameObject);
	}

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}
}
