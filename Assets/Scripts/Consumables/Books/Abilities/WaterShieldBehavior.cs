using UnityEngine;
using System.Collections;

public class WaterShieldBehavior : MonoBehaviour {

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

	}

	private void OnCollisionEnter(Collision collision) {
		Transform child = gameObject.transform.GetChild(0);
		ParticleSystem particle = child.GetComponent<ParticleSystem>();
		if (!particle.isPlaying) {
			particle.Play();
		}
	}

	// Update is called once per frame
	void Update() {

	}
}
