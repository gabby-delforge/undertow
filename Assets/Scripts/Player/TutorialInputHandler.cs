using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TutorialInputHandler : InputHandler
{
    int tutorialIndex = 0;
    int[,] tut1 = new int[,] { { 0, 8, 7,7}, { 1, 9, 7,7 }, { 2, 10, 7,7 }, { 3, 11, 7,7 }, { 4, 11, 8, 7 }, { 5, 11, 8, 7 }, { 6, 6, 6, 6 } };
    //int[,] tut2 = new int[,] { { 0, 0, 1, 5 }, {0, 0, 2, 6 }, { 0, 3, 6, 7 }, { 0, 4, 6, 7 }, { 8, 8, 8, 8 } };
    //isTut1public bool isTut1 = false;
    public int lastTip;
    int[,] indexToMask;
    public List<GameObject> tutMasks;

    public void Start()
    {
       // if (isTut1)
        //{
            indexToMask = tut1;
       // }
       // else
       // {
       //     indexToMask = tut2;
        //}
        GoToIndex(0);
    }

    public void ToggleID(int groupID)
    {
        playerInput[groupID].Invoke();
    }

    void GoToIndex(int ind)
    {
        tutorialIndex = ind;
        for(int i=0; i<tutMasks.Count; i++)
        {
            tutMasks[i].SetActive(false);
        }
        if (!(ind >= 0 && ind < indexToMask.GetLength(0)))
            return;
        int t;
        for (int j=0; j<indexToMask.GetLength(1); j++)
        {
            t = indexToMask[ind,j];
            if (!(t >= 0 && t < tutMasks.Count))
                continue;
            tutMasks[t].SetActive(true);
        }

    }
    public void NextTip()
    {
        GoToIndex(tutorialIndex + 1);
    }

    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                CheckForHit(Input.GetTouch(i).position);
            }
        }
        

        //CODE FOR MOUSE PRESSES

        if (Application.isEditor && Input.GetMouseButtonDown(0))
        {
            CheckForHit(Input.mousePosition);
        }

    }

    void CheckForHit(Vector2 touch)
    {
        if (tutorialIndex < lastTip)
        {
            GoToIndex(tutorialIndex + 1);
            return;
        }
        if (tutorialIndex == lastTip)
            GoToIndex(tutorialIndex + 1);

        Vector3 touchPosWorld = Camera.main.ScreenToWorldPoint(touch);
        RaycastHit2D[] hitInfos = Physics2D.RaycastAll((Vector2)touchPosWorld, Camera.main.transform.forward, Mathf.Infinity, 512);
        if (hitInfos.Length <= 0)
            return;
        RaycastHit2D hitInformation = hitInfos[0];
        foreach (RaycastHit2D hit in hitInfos)
        {
            if (Vector3.Distance(touchPosWorld, hit.collider.transform.position) < Vector3.Distance(touchPosWorld, hitInformation.collider.transform.position))
                hitInformation = hit;
        }
        GameObject touchedObject = hitInformation.collider.transform.parent.gameObject;
        GroupIDs touched = touchedObject.GetComponent<GroupIDs>();
        if (touched != null)
        {
            foreach (int groupID in touched.groupIDs)
            {
                ToggleID(groupID);
            }
        }

        //hacky code for hacky anchor
        PlayerManager hitPlayer = touchedObject.GetComponent<PlayerManager>();
        if (hitPlayer != null && hitPlayer.gameObject.transform.Find("The Chain") != null)
        {
            hitPlayer.gameObject.transform.Find("The Chain").GetComponent<Chain>().Exit();
            GetComponent<AudioSource>().PlayOneShot(chainDestroySound);
        }

    }
}
