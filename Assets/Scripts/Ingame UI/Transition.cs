using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour {
    public static Transition t;
    public float time;
    Image im;
    private void Awake()
    {
        t = this;
        im = GetComponent<Image>();
        im.color = new Color(im.color.r, im.color.g, im.color.b, 1f);

        // SceneManager.sceneLoaded += this.LoadedCallback;

        im.CrossFadeAlpha(0f, time, true);
    }

    

    public void LoadedCallback(Scene scene, LoadSceneMode sceneMode)
    {
        
        im.CrossFadeAlpha(0f, time, true);
    }

    public void FadeToScene(int sceneID)
    {
        if (sceneID == 0)
        {
            SceneManager.LoadScene(0);
            return;
        }
        im.CrossFadeAlpha(1f, time, true);
        StartCoroutine(DelayedSceneChange(time, sceneID));
    }
    
    
    public static IEnumerator DelayedSceneChange(float t, int sceneID)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + t)
        {
            yield return null;
        }
        SceneManager.LoadScene(sceneID);
    }
}
