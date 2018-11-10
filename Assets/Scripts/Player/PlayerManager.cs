using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    public Vector3 spawnPoint;
    public AudioClip deathNoise;
    Rigidbody2D rb;
    SpriteRenderer sr;
    ParticleSystem ps;
    Vector3 psX;
    private bool alive;
    private bool hasBeenRendered;
    GameObject deathBubbles;
    AudioSource audio;
    public float deathVolume = 0.8f;
    
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        ps = GetComponentInChildren<ParticleSystem>();
        deathBubbles = GameObject.Find("DeathBubbles");
        psX = ps.transform.localPosition;
        alive = true;
        hasBeenRendered = false;
        audio = GetComponent<AudioSource>();
        audio.Play();
	}

    void FixedUpdate()
    {
        if (transform.Find("The Chain") == null) {
            float angle = Mathf.Rad2Deg * Mathf.Atan2(rb.velocity.y, rb.velocity.x);
            if (Mathf.Abs(angle) > 90)
            {
                sr.flipX = true;
                angle += 180;
                ps.transform.localPosition = -psX;
            }
            else
            {
                sr.flipX = false;
                ps.transform.localPosition = psX;
            }        
            sr.transform.eulerAngles = new Vector3(0, 0, angle);

            if (alive) 
                audio.volume = Vector3.Magnitude(rb.velocity) / 20f;

        }

        if (!hasBeenRendered)
            hasBeenRendered = gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().isVisible;

        if (hasBeenRendered && alive && !gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().isVisible)
            Respawn ();

        
        

    }

	public void Damage () {
		Respawn ();
	}

	public void ReachGoal () {
        StarController sc = StarController.sc;
        if(sc!=null)
            sc.SaveStarData ();
        int curLvl = sc.GetLevelIndex(SceneData.sd.levelID);
        SceneData.sd.goToLevelSelect = true;
        audio.Stop();
        foreach (HeadToggle h in FindObjectsOfType(typeof(HeadToggle))) {
            h.GetComponent<AudioSource>().volume = 0;
        }
        VictoryOverlay.vo.Activate(sc.CollectedStarsInLevel(curLvl),sc.TotalStarsInLevel(curLvl));
        //VictoryOverlay.vo.Activate(1,4);
        //SceneManager.LoadScene (0);	
    }

    IEnumerator WaitThenRestart(float time) {
        yield return new WaitForSeconds(time);

        StarController.sc.LoadStarData();
        Scene loadedLevel = SceneManager.GetActiveScene();
        Transition.t.FadeToScene(loadedLevel.buildIndex);

    }

    private void Respawn()
    {
        alive = false;
        //ParticleSystem.EmitParams ep = new ParticleSystem.EmitParams();
        //ep.
        deathBubbles.transform.position = transform.position;
        deathBubbles.gameObject.GetComponent<ParticleSystem>().Play();
        transform.Find("Sprite").GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        //GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //GetComponent<GravityEffected>().SetMass(0f);
        GetComponent<CapsuleCollider2D>().enabled = false;
        transform.Find("Sprite").Find("Bubbles").gameObject.GetComponent<ParticleSystem>().Stop();
        audio.Stop ();
        audio.volume = deathVolume;
        audio.PlayOneShot(deathNoise);
        StartCoroutine(WaitThenRestart (1.1f));
    }

    IEnumerator Freeze(float duration)
    {
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(duration);
        rb.constraints = RigidbodyConstraints2D.None;
    }
}
