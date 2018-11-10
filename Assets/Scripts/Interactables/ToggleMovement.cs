using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMovement : Toggleable {

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
	float timeCounter;
    bool moving = false;

	// Use this for initialization
	void Start () {
        base.Start();
		startYPos = transform.position.y;
        startXPos = transform.position.x;
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
        anim.SetBool ("isActive", GetComponent<GroupIDs>().isActive);
		prevPos = new Vector2(transform.position.x, transform.position.y);
	}

    public override void Toggle(bool isActive) {
        moving = isActive;
        if (anim != null) 
	        anim.SetBool ("isActive", isActive);
    }
	
	// Update is called once per frame
	void Update () {
        if (moving) {
            transform.position =  new Vector2((startXPos +  amplitudeX*Mathf.Sin((speedX * 0.1f)*timeCounter)), (startYPos +  amplitudeY*Mathf.Sin((speedY * 0.1f)*timeCounter)));
            prevPos = transform.position;
            timeCounter += Time.deltaTime;
        }
	}
}
