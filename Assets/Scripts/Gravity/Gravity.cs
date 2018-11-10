using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {

    [SerializeField]
    private float mass;

    protected GravityManager gm;
    GroupIDs gid;

    protected virtual void Start ()
    {
        gm = GravityManager.gm;
        gid = GetComponent<GroupIDs> ();

    }

    public float GetMass ()
    {
        return mass;
    }

    public void SetMass(float _mass)
    {
        mass = _mass;
    }

    public bool IsActive ()
    {
        if (gid != null)
        {
            return gid.IsActive ();
        }
        return true;
    }

    public void SetActive (bool isActive)
    {
        if (gid != null)
        {
            gid.SetActive (isActive);
        }
    }
}
