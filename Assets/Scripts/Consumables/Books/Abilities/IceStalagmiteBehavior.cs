using UnityEngine;
using System.Collections;
using Characters.Interfaces;

public class IceStalagmiteBehavior : MonoBehaviour {

	// Use this for initialization
	void Start() {

	}

	private void OnCollisionEnter(Collision collision) {
		Character enemy = collision.gameObject.GetComponent<Character>();
		if (enemy != null) {
			Animation animation = gameObject.GetComponent<Animation>();
			if (!animation.isPlaying) {
				animation.Play();
			}
			Character.Damage iceStalagmiteDamage = new Character.Damage(200.0F, EnumUtility.AttackType.Niflheim);
			Debug.Log(enemy + " hitted");
			enemy.SendMessage("TakeDamage", iceStalagmiteDamage, SendMessageOptions.DontRequireReceiver);
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
