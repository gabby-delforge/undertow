using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPageSelector : MonoBehaviour {

    public List<GameObject> panels;
    public List<GameObject> heads;
    public GameObject redHead;
    // Use this for initialization
    void Start () {
        redHead.SetActive(true);
        foreach(GameObject head in heads)
        {
            head.SetActive(false);
        }
        if (SceneData.sd != null &&  SceneData.sd.goToLevelSelect) {
            SwitchToPanel(1);
            SceneData.sd.goToLevelSelect = false;
        } else {
            SwitchToPanel(0);
        }
        Time.timeScale = .75f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SwitchToPanel(int id)
    {
        for(int i = 0; i < panels.Count; i++)
        {
            panels[i].SetActive(false);
        }
        panels[id].SetActive(true);
        foreach (GameObject head in heads)
        {
            head.SetActive(id == 2);
        }
        redHead.SetActive(id==0);
    }

    public void ResetStars () {
        //StarController.sc.ResetStars();
        Debug.Log("Shouldn't happen");
    }

    public void CollectAllStars () {
        //StarController.sc.CollectAllStars();
        Debug.Log("Shouldn't happen");
    }
}
