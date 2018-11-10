using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityEffected : Gravity {

    protected override void Start()
    {
        base.Start();
        gm.AddToEffected(this);
    }

}
