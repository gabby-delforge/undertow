using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour {
	public AudioClip menuMusic;
	public AudioClip levelMusic;
	private AudioSource audioSource;
	private bool onMenu = true;
	public float fadeTime = 0.4f;

	public static MusicController mc;

	void Awake()
	{
		if (mc == null) {
			mc = this;
			DontDestroyOnLoad(gameObject);
			audioSource = GetComponent<AudioSource> ();
		} else if (mc != this) {
			Destroy(gameObject);
		}
	}


}
