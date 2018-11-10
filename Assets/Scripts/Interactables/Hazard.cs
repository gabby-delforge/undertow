using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hazard : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D col) {
        if (col.collider.name != "Touch Hitbox") {
            PlayerManager player = col.gameObject.GetComponent<PlayerManager> ();	
            if (player != null)
                AffectPlayer (player);
        }
	}

	protected abstract void AffectPlayer (PlayerManager player);
}
