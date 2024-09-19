using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class SubGoal
{
    public Dictionary<string, int> SubGoals;
    public bool remove;

    public SubGoal(string goalName, int value, bool isRemoving)
    {
        SubGoals = new Dictionary<string, int>();
        SubGoals.Add(goalName, value);
        remove = isRemoving;
    }
}
public class Agent : MonoBehaviour
{
    public List<GameAction> actions =  new List<GameAction>();
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();

    GamePlanner planner;
    Queue<GameAction> actionQueue;
    public GameAction currentAction;
    SubGoal currentGoal;

    private void Start()
    {
        GameAction[] acts = this.GetComponents<GameAction>();

        foreach (GameAction act in acts) 
        { 
            actions.Add(act);
        }
    }

    private void LateUpdate()
    {
        
    }
}
