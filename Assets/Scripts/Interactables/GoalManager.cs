using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour {
    //public SubGoal goalGameObject;
    public static GoalManager gm;
    void Awake()
    {
        gm = this;
    }
    void Start()
    {

    }

    public void TriggerGoal(Collider2D col)
    {
        PlayerManager player = col.gameObject.GetComponent<PlayerManager>();
        if (player == null)
            return;
        player.ReachGoal();
    }
    /*int currentGoalIndex;
    [HideInInspector]
    public List<GameObject> goalGameObjects;
    public static GoalManager gm;
    List<SubGoal> subGoals;
    void Awake()
    {
        gm = this;
    }
	void Start () {
        subGoals = new List<SubGoal>();
		foreach (GameObject goal in goalGameObjects)
        {
            subGoals.Add(goal.GetComponent<SubGoal>());
        }
        foreach (SubGoal goal in subGoals)
        {
            goal.Deactivate();
        }
        subGoals[0].Activate();
    }
	
	public void TriggerGoal(Collider2D col)
    {
        PlayerManager player = col.gameObject.GetComponent<PlayerManager>();
        if (player == null)
            return;
        subGoals[currentGoalIndex].Deactivate();
        currentGoalIndex += 1;
        if (currentGoalIndex >= subGoals.Count)
        {
            player.ReachGoal();
            return;
        }
        subGoals[currentGoalIndex].Activate();
    }*/
}
