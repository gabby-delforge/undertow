using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GravityManager : MonoBehaviour {
    public float G;
    public float minimumForceThreshold;

    [HideInInspector]
    public static GravityManager gm;

    List<GravityAffector> sources;
    List<GravityEffected> effected;
    void Awake () {
        gm = this;

    }

    private void OnEnable()
    {
        sources = new List<GravityAffector>();
        effected = new List<GravityEffected>();
    }

    public void ClearLists()
    {

        sources.Clear();
        effected.Clear();
    }

    public void AddToSources(GravityAffector source)
    {
        sources.Add(source);
    }
    public void AddToEffected(GravityEffected effectee)
    {
        effected.Add(effectee);
    }



    void FixedUpdate () {

		foreach (GravityAffector  source in sources) {
			if (!source.IsActive())
				continue;
			foreach (GravityEffected target in effected)
            {
                if (!target.IsActive())
					continue;
				ApplyGravity (source, target);

			}
		}
	}

    void ApplyGravity(GravityAffector affector, GravityEffected effected)
    {
        Rigidbody2D rb = effected.GetComponent<Rigidbody2D>();
        float distSq = Mathf.Pow(Vector2.Distance(affector.transform.position, effected.transform.position),2);
        Vector2 dir = affector.transform.position - effected.transform.position;
        dir = dir.normalized * (G*affector.GetMass()*effected.GetMass()/distSq);
        if(dir.magnitude>minimumForceThreshold)
            rb.AddForce(dir);

    }
}
