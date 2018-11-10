using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSprite : Toggleable
{
	Animator anim;
    Color baseColor;
    Collider2D coll;

    public int SpriteID;
    public bool topDown; //For demo purposes

    protected override void Start()
    {
        anim = GetComponent<Animator>();
        if (anim != null) 
            anim.SetBool ("isActive", GetComponent<GroupIDs>().isActive);

        base.Start();
    }

    public override void Toggle(bool isActive)
    {
		base.Toggle (isActive);
		if (anim != null)
			anim.SetBool ("isActive", isActive);
    }
}
