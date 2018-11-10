using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferMovement : MonoBehaviour {

	float startYPos;
    float startXPos;
	public float amplitudeX;
    public float amplitudeY;
	public float speedY = 0f;
    public float speedX = 0f;
	float freq;
	Rigidbody2D rb;
	Animator anim;
	Vector2 prevPos;
	float start;

	// Use this for initialization
	void Start () {
		startYPos = transform.position.y;
        startXPos = transform.position.x;
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		prevPos = new Vector2(transform.position.x, transform.position.y);
		start = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (speedX != 0 || speedY != 0) {
            transform.position =  new Vector2((startXPos +  amplitudeX*Mathf.Sin((speedX * 0.1f)*(Time.time - start))), (startYPos +  amplitudeY*Mathf.Sin((speedY * 0.1f)*(Time.time - start))));
            prevPos = transform.position;
        }
	}
}
