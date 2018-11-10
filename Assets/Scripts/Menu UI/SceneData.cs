using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour {
    public int levelID;
    public bool goToLevelSelect = false;
    [HideInInspector]
    public float scrollBarVal = 1f;


    public static SceneData sd;
	void Awake () {
        if (sd == null) {
            sd = this;
            DontDestroyOnLoad(gameObject);
        } else if (sd != this) {
            Destroy(gameObject);
        }
    }
}
