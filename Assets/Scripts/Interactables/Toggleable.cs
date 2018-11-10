using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggleable : MonoBehaviour {

	int inactiveLayer = 8;
    int activeLayer = 0;

	protected virtual void Start()
    {
		GetComponent<GroupIDs>().AddToListener(Toggle);
    }

    public virtual void Toggle(bool isActive)
    {
        gameObject.layer = isActive ? activeLayer : inactiveLayer;
    }
}
