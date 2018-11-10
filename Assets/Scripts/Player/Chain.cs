using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour {

    Vector3 startPos;
    Transform daddy;
    public float radius = 0.05f;
    float pullStrength = 0.5f;

	// Use this for initialization
	void Start () {
        daddy = transform.parent;
        startPos = daddy.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.parent != null) {
            float distance = Vector3.Distance(daddy.transform.position, startPos); //distance from ~green object~ to *black circle*
            if (distance > (radius)) //If the distance is less than the radius, it is already within the circle.
            {
                Vector3 fromOriginToObject = daddy.transform.position - startPos; //~GreenPosition~ - *BlackCenter*
                fromOriginToObject = fromOriginToObject.normalized * radius; //Multiply by radius //Divide by Distance
                daddy.transform.position = startPos + fromOriginToObject; //*BlackCenter* + all that Math
                daddy.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            }else
            if (daddy.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude == 0) {
                Vector3 fromOriginToObject = (startPos - daddy.transform.position).normalized;
                daddy.transform.position += Time.deltaTime * fromOriginToObject * pullStrength;

            }
        } else {
            
            transform.position = new Vector3(transform.position.x - 0.25f, transform.position.y, transform.position.z);
            if (transform.position.x < -100)
                Destroy(gameObject);
        }
	}

    public void Exit () {
        transform.parent.gameObject.GetComponent<Floater>().on = false;
        Destroy(transform.parent.Find("Touch Hitbox").gameObject);
        transform.parent = null;
    }
}
