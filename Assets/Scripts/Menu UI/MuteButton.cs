using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteButton : MonoBehaviour {

	public void OnPress()
    {
        AudioController.ac.ToggleMute();
    }

    public void Start()
    {
        AudioController.ac.UpdateChild(gameObject);
    }
}
