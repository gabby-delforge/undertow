using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleRotation : Toggleable {
    bool rotating = false;
    public float speed = 50f;
    Animator anim;

    protected override void Start () {
        anim = GetComponent<Animator>();
        if (anim != null) 
            anim.SetBool ("isActive", GetComponent<GroupIDs>().isActive);
        base.Start();

    }

	public override void Toggle(bool isActive) {
        rotating = isActive;
        if (anim != null)
			anim.SetBool ("isActive", isActive);

	}

    void Update () {
        if (rotating)
            transform.Rotate(new Vector3(0,0,1)* Time.deltaTime * speed);
    }
}
