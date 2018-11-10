using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHazard : Hazard {

	protected override void AffectPlayer (PlayerManager player)
	{
		player.Damage ();
	}
}
