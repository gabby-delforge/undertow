using UnityEngine;
using System.Collections;

public class DrawAnchor : MonoBehaviour {
	public GameObject target; // the objects to draw the line between
	// Use this for initialization
	private bool draw = true;
	private LineRenderer l;
	void Start () {
		l =this.GetComponent<LineRenderer>();
		Vector2 p1 = transform.position;
		l.SetPosition (0, p1);
		if (target == null) 
			TurnOff ();
	}

	void OnJointBreak2D (Joint2D brokenJoint) {
		TurnOff ();
	}

	private void TurnOff () {
		this.enabled = false;
		this.GetComponent<LineRenderer> ().enabled = false;
		this.GetComponent<DistanceJoint2D> ().enabled = false;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (draw) {
			l.SetPosition (0, target.transform.position);
			l.SetPosition (1, transform.position);
		}
	}
}
