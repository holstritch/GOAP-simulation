using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class Node
{
    public Node parent;
    public float cost;
    public Dictionary<string, int> state;
    public GameAction action;

    public Node(Node parent, float cost, Dictionary<string, int> allStates, GameAction action)
    {
        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(allStates); //copy of allStates dic
        this.action = action;
    }
}
public class GamePlanner : MonoBehaviour
{
    public Queue<GameAction> plan(List<GameAction> actions, Dictionary<string, int> goal, WorldStates states)
    {
        // find which action is usable
        List<GameAction> usableActions = new List<GameAction>();
        foreach (GameAction action in actions)
        {
            if (action.IsAchieveable())
            {
                usableActions.Add(action);
            }
        }

        // create leaf of graph
        List<Node> leaves = new List<Node>();
        Node start = new Node(null, 0, GameWorld.Instance.GetWorld().GetStates(), null);

        bool success = BuildGraph(start, leaves, usableActions, goal);

        if (!success) 
        {
            Debug.Log("NO PLAN");
            return null;
        }

        // plan has been found - find cheapest
        Node cheapest = null;
        foreach (Node leaf in leaves)
        {
            if (cheapest == null)
                cheapest = leaf;
            else if (leaf.cost < cheapest.cost)
                cheapest = leaf;
        }

        // found cheapest leaf node - work backwards to get sequence of actions
        List<GameAction> result = new List<GameAction>();
        Node n = cheapest;
        // get nodes action and parent then repeat on the parent
        while (n != null)
        {
            if (n.action != null) 
                result.Insert(0, n.action);
            
            n = n.parent;
        }

        // get action from list to add to queue
        Queue<GameAction> queue = new Queue<GameAction>();
        foreach(GameAction action in result)
        {
            queue.Enqueue(action);
        }

        Debug.Log("The Plan is : ");
        foreach (GameAction action in queue)
        {
            Debug.Log("Q: " + action.actionName);
        }

        return queue;
    }

    // recursion - method calls itself
    private bool BuildGraph(Node parent, List<Node> leaves, List<GameAction> usableActions, Dictionary<string, int> goal)
    {
        bool foundPath = false;
        foreach (GameAction action in usableActions)
        {
            if (action.IsAchieveableGiven(parent.state))
            {
                // new dic is being copied from parent.state - which is a list of all the world states
                // as we go through actions and build the branch we can match these to this dic
                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);
                foreach(KeyValuePair<string, int> effects in action.afterEffectsDic)
                {
                    if (!currentState.ContainsKey(effects.Key))
                        currentState.Add(effects.Key, effects.Value);
                }

                // cost value increases 
                Node node = new Node (parent, parent.cost + action.cost, currentState, action);
                if (GoalAchieved(goal, currentState))
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                else // havent found a path, go to next node
                {
                    List<GameAction> subset = CreateActionSubset(usableActions, action);
                    // recursive call 
                    // remove action that was just checked - so list gets smaller each time
                    bool found = BuildGraph(node, leaves, subset, goal);
                    if (found)
                        foundPath = true;
                }
            }
        }
        return foundPath;
    }

    private bool GoalAchieved(Dictionary<string, int> goals, Dictionary<string, int> states)
    {
        foreach(KeyValuePair<string, int> goal in goals)
        {
            if (!states.ContainsKey(goal.Key))
                return false;
        }
        return true;
    }

    private List<GameAction> CreateActionSubset(List<GameAction> actions, GameAction actionToRemove)
    {
        List<GameAction> subset = new List<GameAction>();
        foreach (GameAction action in actions)
        {
            // only adds actions that aren't the action to remove
            if (!action.Equals(actionToRemove))
                subset.Add(action);
        }
        return subset;
    }
}
