using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarSaver : MonoBehaviour {
    
	void Start () {
        Scrollbar temp = GetComponent<Scrollbar>();
        if(temp!=null)
            temp.value = SceneData.sd.scrollBarVal;
	}
	
	void Update () {
		
	}

    public void ScrollbarChanged()
    {
        SceneData.sd.scrollBarVal = GetComponent<Scrollbar>().value;
    }
}
