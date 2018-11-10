using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {
	public float speed = 0.5f;
	private Vector3 startPosition;
	public Vector3 endPosition;

	void Start () {
		startPosition = transform.position;
	}

	void Update () {
		transform.position = Vector3.Lerp (startPosition, endPosition, Mathf.PingPong (Time.time * speed, 1.0f));
	}
}
