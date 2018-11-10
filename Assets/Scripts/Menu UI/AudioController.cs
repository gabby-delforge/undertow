using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour {

    public static AudioController ac;
    float volume = 1;
    public bool mute = false;
    public Sprite muteIMG;
    public Sprite unmuteIMG;
	private bool onMenu = false;
	public AudioClip levelMusic;
	public AudioClip menuMusic;
	private AudioSource audioSource;
	public float fadeTime = 0.4f;
    void Awake()
    {
        if (ac == null)
        {
            ac = this;
            DontDestroyOnLoad(gameObject);
			audioSource = GetComponent<AudioSource> ();
        }
        else if (ac != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {

        mute = !mute;
        ToggleMute(); 
    }

    public void PlayLevelMusic () {
		onMenu = false;
		StartCoroutine ("FadeOut");
	}

	public void PlayMenuMusic () {
		onMenu = true;
		StartCoroutine ("FadeOut");
	}


	public IEnumerator FadeOut () {
		float startVolume = audioSource.volume;
		while (audioSource.volume > 0) {
			audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
			yield return null;
		}

		audioSource.Stop ();
		audioSource.volume = startVolume;
		if (onMenu) {
			audioSource.clip = menuMusic;
			audioSource.Play ();
		} else {
			audioSource.clip = levelMusic;
			audioSource.Play ();
		}
	}

    public void BackgroundVolume () {
        audioSource.volume = 0.3f;
    }

    public void ForegroundVolume () {
        audioSource.volume = 0.6f;
    }
    
    public void ToggleMute()
    {
        mute = !mute;
        SetVolume(volume);
        MuteButton[] objs = Resources.FindObjectsOfTypeAll<MuteButton>();
        foreach(MuteButton obj in objs)
        {
            UpdateChild(obj.gameObject);
        }
    }

    public void UpdateChild(GameObject child)
    {
        child.GetComponentsInChildren<Image>()[1].sprite = mute ? muteIMG : unmuteIMG;
    }

    public void SetVolume(float vol)
    {
        volume = vol;
        AudioListener.volume = mute ? 0 : volume;
    }
    
}
