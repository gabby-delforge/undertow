using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour {

    public float cycleDuration;
    public float amplitude;
    Vector2 maxAnch;
    Vector2 minAnch;
    RectTransform rt;
    float elapsedTime = 0f;
	void Start ()
    {
        rt = GetComponent<RectTransform>();
        maxAnch = rt.anchorMax;
        minAnch = rt.anchorMin;
	}

    private void OnEnable()
    {
        //elapsedTime = 0f;
        rt = GetComponent<RectTransform>();
        maxAnch = rt.anchorMax;
        minAnch = rt.anchorMin;
    }

    // Update is called once per frame
    void Update () {
        elapsedTime += Time.deltaTime;
        float delta = Mathf.Sin(elapsedTime / cycleDuration * 2 * Mathf.PI) * amplitude;
        rt.anchorMax = maxAnch + Vector2.up * delta;
        rt.anchorMin = minAnch + Vector2.up * delta;
    }
}
