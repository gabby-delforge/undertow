using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour {

    Vector3 startPos;
    Vector3 endPos;
    public float timeToCycle = 1f;
    public float offsetTime = .5f;
    public float cycleDelay = 0f;
    float currTime = 0f;
	void Start () {
        startPos = GetCamPos(-.1f, .5f);
        endPos = GetCamPos(1.1f, .5f);
        currTime -= offsetTime;
	}

    Vector3 GetCamPos(float x, float y)
    {
        float xPos = Screen.width * x;
        float yPos = Screen.height * y;
        return Camera.main.ScreenToWorldPoint(new Vector3(xPos, yPos, 0));
    }
	Vector3 Lerp(Vector3 a, Vector3 b, float t)
    {
        return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, 0);
    }
	void Update () {
        currTime += Time.deltaTime;
        if (currTime >= timeToCycle)
            currTime = -cycleDelay;
        transform.position = Lerp(startPos, endPos, currTime / timeToCycle);
	}
}
