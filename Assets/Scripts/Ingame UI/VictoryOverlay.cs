using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryOverlay : MonoBehaviour {

    public static VictoryOverlay vo;
    public Color inactiveStarColor;
    public Color activeStarColor;
    public GameObject starPrefab;
    public GameObject starPanel;
    public GameObject deactivateOnVictory;
    public GameObject deactivateOnVictory2;
    public float entranceTime;
    public AudioClip victoryTune;

    public GameObject nextLevelButton;
    public Text congratulatoryText;

	void Awake () {
        vo = this;
	}
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void Activate(int collectedStars, int maxStars)
    {
        // gameObject.SetActive(false);
        deactivateOnVictory.SetActive(false);
        deactivateOnVictory2.SetActive(false);
        Time.timeScale = 0;
        for (int i = 0; i < maxStars; i++)
        {
            GameObject star = Instantiate(starPrefab);
            star.transform.SetParent(starPanel.transform);
            star.transform.localScale = new Vector3(1f, 1f, 1f);
            star.GetComponent<Image>().color = i < collectedStars ? activeStarColor : inactiveStarColor;

        }
        gameObject.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(victoryTune);
        AudioController.ac.BackgroundVolume();
        StarController sc = StarController.sc;


        if (sc.GetLevelIndex(SceneData.sd.levelID) >= sc.starData.allStars.Count - 1) // you beat the game. somehow
        {
            //nextLevelButton.GetComponent<Image>().color = new Color(.3f, .3f, .3f, 1f);
            nextLevelButton.GetComponent<Button>().interactable = false;
            nextLevelButton.transform.GetChild(0).gameObject.SetActive(false);

            starPanel.SetActive(false);
            congratulatoryText.text = "Thanks for playing!";
        } else if (sc.starData.TotalStarsGotten() < sc.starData.allStars[sc.GetLevelIndex(SceneData.sd.levelID) + 1].starRequirement) { 
            //nextLevelButton.GetComponent<Image>().color = new Color(.3f, .3f, .3f, 1f);
            nextLevelButton.GetComponent<Button>().interactable = false;
            nextLevelButton.transform.GetChild(0).gameObject.SetActive(false);
        }
        StartCoroutine(Entrance());
    }

    IEnumerator Entrance()
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + entranceTime)
        {
            SetHeight(1f - (Time.realtimeSinceStartup - start)/entranceTime);
            yield return null;
        }
    }

    void SetHeight(float bottom)
    {
        GetComponent<RectTransform>().anchorMin = new Vector2(0f, bottom);
        GetComponent<RectTransform>().anchorMax = new Vector2(1f, bottom+1f);
    }

}
