using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillLevelSelect : MonoBehaviour {
    List<StarData.LevelStars> allStars;
    public GameObject rowPref;
    public GameObject buttonPref;
    public GameObject scrollContainer;
    public Text totalStarCount;
    // Use this for initialization
    void Start ()
    {
        StarData sd = StarController.sc.starData;
        allStars = sd.allStars;
        int counter = 0;
        GameObject tempObj;
        GameObject tempRow = null;
        LevelButton tempLvlBtn;
        int totalStars = sd.TotalStarsGotten();
        totalStarCount.text = totalStars.ToString();
        int rowSize = 5;
        while (counter < allStars.Count)
        {
            if (counter % rowSize == 0)
            {
                tempRow = Instantiate(rowPref);
                tempRow.transform.SetParent(scrollContainer.transform);
                tempRow.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            tempObj = Instantiate(buttonPref);
            tempObj.transform.SetParent(tempRow.transform);
            tempObj.transform.localScale = new Vector3(1f, 1f, 1f);
            tempLvlBtn = tempObj.GetComponentInChildren<LevelButton>();
            tempLvlBtn._Init(counter + 1, allStars[counter].levelId, allStars[counter].CountStars(), allStars[counter].stars.Count,allStars[counter].starRequirement -totalStars);
            if(counter==0)
                tempObj.GetComponentInChildren<LevelButton>().SetTutorialIndex(1);
            if (counter == 1)
                tempObj.GetComponentInChildren<LevelButton>().SetTutorialIndex(2);
            counter++;
        }
        /*while (counter % rowSize != 0)
        {
            tempRow.transform.GetChild(counter % rowSize).gameObject.SetActive(false);
            counter++;
        }*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
