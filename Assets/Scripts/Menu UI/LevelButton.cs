using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour {
    int number;
    int levelID;
    int numStars;
    int maxStars;
    Text text;
    public GameObject locked;
    public GameObject unlocked;
    public AudioClip buttonPress;
    public List<Sprite> sprites;
    bool isLocked;
    int starsTillUnlock;
    int tutorialIndex = -1;
    public void _Init(int _number, int _levelID, int _numStars, int _maxStars, int _starsTillUnlock) //Called during intialization between Awake and Start
    {
        number = _number;
        levelID = _levelID;
        numStars = _numStars;
        starsTillUnlock = _starsTillUnlock;
        isLocked = _starsTillUnlock > 0;
        maxStars = _maxStars;
    }
    void Start () {
        IsLocked(isLocked);
        SetStarCount(numStars, maxStars);
        SetLevel(number);
        SetStarsToUnlock(starsTillUnlock);
        GetComponentInChildren<Image>().sprite = sprites[Random.Range(0, sprites.Count)];
    }
    public void SetTutorialIndex(int i)
    {
        tutorialIndex = i;
    }
    public void Pressed()
    {
        if (isLocked)
            return;
        Camera.main.GetComponent<AudioSource>().PlayOneShot(buttonPress);
		AudioController.ac.PlayLevelMusic ();
        SceneData.sd.levelID = levelID;
        if (tutorialIndex==1)
            SceneManager.LoadScene("Tutorial");
        else
            SceneManager.LoadScene("LoadLevels");
    }
    public void IsLocked(bool val)
    {
        if (val)
        {
            locked.SetActive(true);
            unlocked.SetActive(false);
        }
        else
        {
            locked.SetActive(false);
            unlocked.SetActive(true);
        }
    }
    public void SetStarCount(int count, int max)
    {
        unlocked.GetComponentInChildren<StarToggler>().SetStarCount(count, max);
    }
    public void SetLevel(int level)
    {
        unlocked.GetComponentInChildren<Text>().text = level.ToString();
    }
    public void SetStarsToUnlock(int remainingStars)
    {
        locked.GetComponentInChildren<Text>().text = remainingStars.ToString();
    }

}
