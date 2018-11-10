using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubGoal : MonoBehaviour {
    Color baseColor;
    public Color inactiveColor;
    GoalManager gm;
    Collider2D coll;
    SpriteRenderer sr;
	void Awake () {
        coll = GetComponent<Collider2D>();
        //sr = GetComponent<SpriteRenderer>();
        //baseColor = sr.color;
	}
    private void Start()
    {
        gm = GoalManager.gm;
        //gm.goalGameObject = this;
    }
    public void Activate()
    {
        //coll.enabled = true;
        //sr.color = baseColor;
    }
    public void Deactivate()
    {
        //coll.enabled = false;
        //sr.color = inactiveColor;
    }
	void OnTriggerEnter2D(Collider2D coll)
    {
        gm.TriggerGoal(coll);
    }
}
