using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadHazard : Hazard {
    protected override void AffectPlayer (PlayerManager player)
	{
        gameObject.GetComponent<HeadToggle>().Toggle(false);
        transform.Find("Whirlpool").GetComponent<WhirlPool>().Toggle(false);
		player.Damage ();
	}
}
