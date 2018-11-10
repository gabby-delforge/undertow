using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemGlow : MonoBehaviour {
    public float glowSpeed;
    Color activeColor;
    SpriteRenderer r;
    GroupIDs parentGroup;
    Gradient g;
    float counter;

    void Start () {
        r = GetComponent<SpriteRenderer>();
        parentGroup = GetComponentInParent<GroupIDs>();
    }
    
	// Update is called once per frame
	void Update () {
        if (g == null ||  activeColor == null || activeColor == new Color(0,0,0,0)) {
            activeColor = GroupIDs.IDToColor[parentGroup.groupIDs[0]];

            GradientColorKey[] gck;
            GradientAlphaKey[] gak;
            g = new Gradient();
            gck = new GradientColorKey[2];
            gck[0].color = Color.gray;
            gck[0].time = 0.0F;
            gck[1].color = activeColor;
            gck[1].time = 1.0F;
            gak = new GradientAlphaKey[2];
            gak[0].alpha = 1.0F;
            gak[0].time = 0.0F;
            gak[1].alpha = 1.0F;
            gak[1].time = 1.0F;
            g.SetKeys(gck, gak);
        }
        if (!parentGroup.isActive) {
            counter += Time.deltaTime;
            r.color = g.Evaluate(((Mathf.Sin(counter * 3.0f) + Mathf.PI / 2.0f) / 2.0f));
        } else {
            r.color = activeColor;
            counter = 0;
        }
		
	}
}
