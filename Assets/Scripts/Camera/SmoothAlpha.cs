using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothAlpha : MonoBehaviour {

	float targetAlpha;
	float maxAlpha;
	bool fading;
	bool fadeIn;
	SpriteRenderer sr;
	public float speed = 0.001f;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		maxAlpha = sr.color.a;
		fading = false;
		fadeIn = false;
		targetAlpha = 1f;

	}
	
	// Update is called once per frame
	void Update () {
		if (!fading) {
			fading = true;
			if (fadeIn) {
				targetAlpha = Random.Range(Mathf.Min(1f, targetAlpha + 0.2f), maxAlpha);
			} else {
				targetAlpha = Random.Range(0f, Mathf.Max(0, targetAlpha - 0.2f));
			}
			Debug.Log ("Starting fade");
			StartCoroutine("Fade");

		}
	}

	IEnumerator Fade() {
		Debug.Log ("inside fade");
		while (Mathf.Abs(sr.color.a - targetAlpha) > 0.1f) {
			Debug.Log ("Fading");
			Color c = sr.color;
			c.a += fadeIn ? speed : -speed;
			sr.color = c;
			yield return null;
		}
		fading = false;
		fadeIn = !fadeIn;
		yield break;
	}

}
