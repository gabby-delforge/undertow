// Floater v0.0.2
// by Donovan Keith (thank you Donovan)
//
// [MIT License](https://opensource.org/licenses/MIT)

using UnityEngine;
using System.Collections;

// Makes objects float up & down while gently spinning.
public class Floater : MonoBehaviour {
	// User Inputs
	//public float degreesPerSecond = 15.0f;
	public float amplitude = 0.5f;
	public float frequency = 1f;
    public bool on = true;
    private float counter;

	// Position Storage Variables
	Vector3 posOffset = new Vector3 ();
	Vector3 tempPos = new Vector3 ();

	// Use this for initialization
	void Start () {
		// Store the starting position & rotation of the object
		posOffset = transform.position;
        counter = Random.Range(0f, 10f);
        PufferMovement pm = GetComponent<PufferMovement>();
        if (pm != null)
            if (pm.speedX != 0 || pm.speedY != 0) 
                enabled = false;
	}

	// Update is called once per frame
	void Update () {
		//Dont wanna rotate in 2D lmao
		//transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

		// Float up/down with a Sin()
        if (on) {

            counter += Time.deltaTime;
            tempPos = posOffset;
            tempPos.y += Mathf.Sin (counter * Mathf.PI * frequency) * amplitude;

            transform.position = tempPos;
        }
	}
}
