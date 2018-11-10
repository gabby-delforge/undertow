using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadToggle : Toggleable {
	Animator anim;
    Color baseColor;
    Collider2D coll;

    AudioSource audio;
	float baseVolume;
    public Color activeColor;
    Color inactiveColor;
    float colorFadeTime = .1f;
	public float fadeTime = 0.5f;
    IEnumerator activeColorCoroutine;

    GameObject spriteMask;


    protected override void Start()
    {
        inactiveColor = Color.gray;
        //GetComponent<SpriteGlow.SpriteGlowEffect>().GlowColor = inactiveColor;
        activeColor = GroupIDs.IDToColor[GetComponent<GroupIDs>().groupIDs[0]];

        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        spriteMask = GetComponentInChildren<SpriteMask>().gameObject;
		baseVolume = audio.volume;
        base.Start();
    }

    public override void Toggle(bool isActive)
    {
		base.Toggle (isActive);
		anim.SetBool ("isActive", isActive);
        if (isActive) {
			StopCoroutine ("FadeOut");
			audio.volume = baseVolume;
			audio.Play ();
            GetComponent<Floater>().on = false;
            spriteMask.SetActive(false);
            /*
            if (activeColorCoroutine!=null)
                StopCoroutine(activeColorCoroutine);
            activeColorCoroutine = ColorLerp(activeColor, colorFadeTime);
            StartCoroutine(activeColorCoroutine);
            */
		} else {
            GetComponent<Floater>().on = true;
			StartCoroutine ("FadeOut");
            spriteMask.SetActive(true);
            /*
            if (activeColorCoroutine != null)
                StopCoroutine(activeColorCoroutine);
            activeColorCoroutine = ColorLerp(inactiveColor, colorFadeTime);
            StartCoroutine(activeColorCoroutine);
            */
        }
    }
    float Lerp(float a, float b, float t)
    {
        return a + (b - a) * t;
    }

    Color Lerp(Color a, Color b, float t)
    {
        return new Color(Lerp(a.r, b.r, t), Lerp(a.g, b.g, t), Lerp(a.b, b.b, t), Lerp(a.a, b.a, t));
    }
    public IEnumerator ColorLerp(Color target, float time)
    {
        SpriteGlow.SpriteGlowEffect sr = GetComponent<SpriteGlow.SpriteGlowEffect>();
        Color startColor = sr.GlowColor;
        float t = 0;

        while (t< time)
        {
            t += Time.deltaTime;
            sr.GlowColor = Lerp(startColor, target, t / time);
            yield return null;
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
