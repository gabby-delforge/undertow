using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseOverlay;
    bool isPaused = false;
    public float timeScale = .75f;

    void OnEnable()
    {
        SetPause(false);
    }
    
    public void TogglePause()
    {
        isPaused = !isPaused;
        if(pauseOverlay!=null)
            pauseOverlay.SetActive(isPaused);
        if (isPaused)
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
        }
        else
        {
            Time.timeScale = timeScale;
            AudioListener.pause = false;
        }        
    }
    public void SetPause(bool pause)
    {
        isPaused = !pause;
        TogglePause();
    }

    public void Restart()
    {
        StarController.sc.LoadStarData();
        AudioController.ac.ForegroundVolume();
        Scene loadedLevel = SceneManager.GetActiveScene();
        Transition.t.FadeToScene(loadedLevel.buildIndex);
    }

    public void BackToMenu()
    {
        StarController.sc.LoadStarData();

        SceneData.sd.goToLevelSelect = true;

        AudioController.ac.ForegroundVolume();
		AudioController.ac.PlayMenuMusic ();
        Transition.t.FadeToScene(0);

    }

    public void NextLevel()
    {
        StarController sc = StarController.sc;
        AudioController.ac.ForegroundVolume();

        int levelIndex = sc.GetLevelIndex(SceneData.sd.levelID);
        levelIndex += 1;
        Debug.Log("Incremented level index : " + levelIndex.ToString());
        if (levelIndex >= sc.starData.allStars.Count || sc.starData.TotalStarsGotten() < sc.starData.allStars[levelIndex].starRequirement) { 
            //BackToMenu();
            return;
        }
        SceneData.sd.levelID = sc.starData.allStars[levelIndex].levelId;
        sc.LoadStarData();
        Transition.t.FadeToScene(1);
    }
}
