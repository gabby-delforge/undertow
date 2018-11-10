using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarToggler : MonoBehaviour {

    public Color offColor;
    public Color onColor;
    public int starCount;
    Image[] stars;
	void Start () {
	}
	public void SetStarCount(int num, int max)
    {
        stars = GetComponentsInChildren<Image>();
        if (max > stars.Length)
        {
            Debug.LogError("Missing stars");
        }
        if (num > max || num < 0)
        {
            Debug.LogError("Star count out of range");
        }
        for(int i = 0; i<max; i++)
        {
            stars[i].gameObject.SetActive(true);
        }
        for (int i = max; i < stars.Length; i++)
        {
            stars[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < num; i++)
        {
            stars[i].color = onColor;
        }
        for(int i =num; i < stars.Length; i++)
        {
            stars[i].color = offColor;
        }
    }
}
