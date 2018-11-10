using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {
	int levelId;
	public int starId;
	StarController s; 
	public Sprite gotten;
	public Sprite notGotten;
    public AudioClip getStarNoise;
    public AudioClip gottenStarNoise;
    public ParticleSystem deathBubbles;
    bool notPicked = true;

	void Start ()
    {
        
        levelId = SceneData.sd.levelID;
        s = StarController.sc;
		if (s.CheckStar (levelId, starId)) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = gotten;
            notPicked = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = notGotten;
        }
	}
    bool dying = false;
	void OnTriggerEnter2D (Collider2D c) {
        if (!dying && c.gameObject.GetComponent<PlayerManager>() != null) {
            dying = true;
            s.ReachStar (levelId, starId);
            GetComponent<SpriteRenderer>().enabled = false;
            if (notPicked) {
                GameObject.Find("Main Camera").GetComponent<AudioSource>().PlayOneShot(getStarNoise);
                deathBubbles.Play();
            } else {
                GameObject.Find("Main Camera").GetComponent<AudioSource>().PlayOneShot(gottenStarNoise);
            }
            Destroy (gameObject, 2f); // same as the bubble particle system lifetime
        }
	}
}
