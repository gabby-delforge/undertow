using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour {
    protected List<UnityEvent> playerInput;
    [HideInInspector]
    public static InputHandler ih;
    public AudioClip chainDestroySound;
    void Awake()
    {
        ih = this;
        playerInput = new List<UnityEvent>();
    }

    public void AddToInputListener(UnityAction call, List<int> groupIDs)
    {
        int groupID;
        for (int i = 0; i < groupIDs.Count; i++)
        {
            groupID= groupIDs[i];
            while (groupID >= playerInput.Count)
            {
                playerInput.Add(new UnityEvent());
            }

            playerInput[groupID].AddListener(call);
        }
    }
    
    public void ToggleID(int groupID)
    {
        playerInput[groupID].Invoke();
    }

    void Update()
    {
        if (Time.timeScale == 0.0f) 
            return;
        
        for(int i = 0; i<Input.touchCount; i++)
        {
            if(Input.GetTouch(i).phase == TouchPhase.Began)
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

        Vector3 touchPosWorld = Camera.main.ScreenToWorldPoint(touch);
		RaycastHit2D[] hitInfos = Physics2D.RaycastAll ((Vector2)touchPosWorld, Camera.main.transform.forward, Mathf.Infinity, 512);
        if (hitInfos.Length <= 0)
            return;
        RaycastHit2D hitInformation = hitInfos[0];
        foreach(RaycastHit2D hit in hitInfos)
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
		if (hitPlayer != null && hitPlayer.gameObject.transform.Find("The Chain") != null) {
            hitPlayer.gameObject.transform.Find("The Chain").GetComponent<Chain>().Exit();
            GetComponent<AudioSource>().PlayOneShot(chainDestroySound);
		}
        
    }
}
