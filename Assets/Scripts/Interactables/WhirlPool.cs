using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlPool : MonoBehaviour {
    
    public float spinSpeed;
    ParticleSystem ps;
    ParticleSystem.MainModule psmm;
    float currentSpinSpeed;
    float fastTimeScale = 3f;
    public ParticleSystem bubbleEmitter;
	void Start () {
        ps = GetComponent<ParticleSystem>();
        psmm = ps.main;
        GroupIDs temp = gameObject.GetComponentInParent<GroupIDs>();
        if (temp != null)
            temp.AddToListener(Toggle);
        else
            Toggle(true);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 rotation = transform.eulerAngles;
        rotation = rotation + Vector3.forward * currentSpinSpeed * Time.deltaTime;
        transform.eulerAngles = rotation;
	}

    public void Toggle(bool isActive)
    {
        if (isActive)
        {
            ps.Play();
            if(bubbleEmitter!=null)
                bubbleEmitter.Play();
            psmm.simulationSpeed = 1f;
            currentSpinSpeed = spinSpeed;
        }
        else
        {
            ps.Stop();
            if (bubbleEmitter != null)
                bubbleEmitter.Stop();
            psmm.simulationSpeed = fastTimeScale;
            //currentSpinSpeed = spinSpeed * fastTimeScale;
        }
    }
}
