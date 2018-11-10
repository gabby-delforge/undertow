using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFlash : MonoBehaviour {

    float startPos = -.2f;
    float endPos = 1.2f;
    float timeToCycle = .25f;
    float offsetTime = .2f;
    float cycleDelay = 5f;

    float currTime = 0f;
    float width;
    RectTransform rt;
    void Start()
    {
        rt = GetComponent<RectTransform>();
        width = rt.anchorMax.x - rt.anchorMin.x;
    }
    private void OnEnable()
    {

        rt = GetComponent<RectTransform>();
        width = rt.anchorMax.x - rt.anchorMin.x;
        StartCoroutine(UpdateLoop());
    }

    float Lerp(float a, float b, float t)
    {
        return a + (b - a) * t;
    }

    IEnumerator UpdateLoop()
    {
        float start = Time.realtimeSinceStartup + offsetTime;
        //rt = GetComponent<RectTransform>();
        while (true)
        {
            currTime = Time.realtimeSinceStartup - start;
            if (currTime >= timeToCycle)
                start = Time.realtimeSinceStartup + cycleDelay;
            currTime = Time.realtimeSinceStartup - start;
            float newM = Lerp(startPos, endPos, currTime / timeToCycle);
           /* while (rt == null)
            {
                rt = GetComponent<RectTransform>();
                yield return null;
            }*/
            rt.anchorMin = new Vector2(newM, rt.anchorMin.y);
            rt.anchorMax = new Vector2(newM + width, rt.anchorMax.y);
            yield return null;
        }
    }
}
