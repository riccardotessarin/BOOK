using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMessage : MonoBehaviour {

	private Transform message;
	private Animation anim;

	private void Awake() {
		message = gameObject.transform.Find("Holo_message");
		message.gameObject.SetActive(false);
		anim = gameObject.GetComponent<Animation>();
	}

	private enum OccilationFuntion { Sine, Cosine }
	private void Start() {
		//to start at zero
		StartCoroutine(Oscillate(OccilationFuntion.Sine, 0.004f));
		//to start at scalar value
		//StartCoroutine (Oscillate (OccilationFuntion.Cosine, 1f));
	}

	private IEnumerator Oscillate(OccilationFuntion method, float scalar) {
		while (true) {
			if (method == OccilationFuntion.Sine) {
				transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time) * scalar + transform.position.y, transform.position.z);
			} else if (method == OccilationFuntion.Cosine) {
				transform.position = new Vector3(transform.position.x, Mathf.Cos(Time.time) * scalar, transform.position.z);
			}
			yield return new WaitForEndOfFrame();
		}
	}

	private void FixedUpdate() {
		//transform.rotation *= Quaternion.AngleAxis(Time.deltaTime * 360f, Vector3.up);
		StartCoroutine(Rotate(1f));
	}

	IEnumerator Rotate(float duration) {
		if(!anim.isPlaying) {	message.gameObject.SetActive(true); }
		Quaternion startRot = message.rotation;
		float t = 0.0f;
		while (t < duration) {
			t += Time.deltaTime;
			message.rotation = startRot * Quaternion.AngleAxis(t / duration * 120f, transform.up); //or transform.right if you want it to be locally based
			yield return null;
		}
		message.rotation = startRot;
	}

	IEnumerator Rotate2(float duration) {
		float startRotation = transform.eulerAngles.y;
		float endRotation = startRotation + 360.0f;
		float t = 0.0f;
		while (t < duration) {
			t += Time.deltaTime;
			float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
			yield return null;
		}
	}


	/*

	public float min = 0.5f;
	public float max = 0.6f;

	void Start() {

		min = transform.position.y;
		max = transform.position.y + 3;

	}

	// Update is called once per frame
	void Update() {


		transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.z);

	}
	*/


	/*

	public Vector3 pointB;

	IEnumerator Start() {
		var pointA = transform.position;
		while (true) {
			yield return StartCoroutine(MoveObject(transform, pointA, pointB, 3.0f));
			yield return StartCoroutine(MoveObject(transform, pointB, pointA, 3.0f));
		}
	}

	IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time) {
		var i = 0.0f;
		var rate = 1.0f / time;
		while (i < 1.0f) {
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
			yield return null;
		}
	}
	*/

}