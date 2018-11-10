using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOnClick : MonoBehaviour {
	public int level;

	public void LoadScene()
	{
		Application.LoadLevel(level);
	}

}
