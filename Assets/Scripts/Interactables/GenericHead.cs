using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericHead : MonoBehaviour {
	public List<GameObject> prefabs;
	public int headID;
	// Use this for initialization
	void Start () {
		GameObject newHead = Instantiate (prefabs [headID], transform.position, transform.rotation);
		newHead.transform.parent = gameObject.transform;
	}
	

}
