using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSound : Toggleable {

	AudioSource audio;
	float baseVolume;
	public float fadeTime = 0.5f;

	protected override void Start()
	{
		audio = GetComponent<AudioSource>();
		baseVolume = audio.volume;
		base.Start();
	}

	public override void Toggle(bool isActive)
	{
		base.Toggle (isActive);
		if (isActive) {
			StopCoroutine ("FadeOut");
			audio.volume = baseVolume;
			audio.Play ();
		} else {
			StartCoroutine ("FadeOut");
		}
			
		
	}

	public IEnumerator FadeOut () {
		float startVolume = audio.volume;

		while (audio.volume > 0) {
			audio.volume -= startVolume * Time.deltaTime / fadeTime;

			yield return null;
		}

		audio.Stop ();
		audio.volume = startVolume;
	}
}
