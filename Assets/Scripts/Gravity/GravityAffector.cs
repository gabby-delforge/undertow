using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAffector : Gravity
{
    protected override void Start()
    {
        base.Start();
        gm.AddToSources(this);
    }

}
