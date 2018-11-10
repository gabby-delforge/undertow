using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResizeBG : MonoBehaviour {

    RectTransform rt;
    float aspectRatio;

    // Use this for initialization
    void Start () {
        rt = GetComponent<RectTransform>();
        aspectRatio = Camera.main.aspect;

        float height = rt.rect.height;
        float width = rt.rect.width;

        float screenHeight = Camera.main.orthographicSize * 2.0f;
        float screenWidth = screenHeight / Screen.height * Screen.width;

        float newScale = aspectRatio < 1.45 ? (screenHeight / height) : (screenWidth / width);

        Vector3 s = new Vector3(newScale, newScale, 1);
        transform.localScale = s;

    }
	
}
