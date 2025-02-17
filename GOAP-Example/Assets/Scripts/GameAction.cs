using System.Collections;
using System.Collections.Generic;
using Agents;
using UnityEngine;
using UnityEngine.AI;

public abstract class GameAction : MonoBehaviour
{
    public string actionName = "Action";
    public float cost = 1.0f;
    public GameObject target;
    public string targetTag;
    public float duration;
    public WorldState[] preConditions;
    public WorldState[] afterEffects;
    public NavMeshAgent agent;

    public Dictionary<string, int> preConditionsDic;
    public Dictionary<string, int> afterEffectsDic;

    public WorldStates agentBeliefs;
    public GameInventory inventory;

    public bool running = false;

    public GameAction()
    {
        preConditionsDic = new Dictionary<string, int>();
        afterEffectsDic = new Dictionary<string, int>();
    }

    private void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();

        if (preConditions != null)
        {
            foreach(WorldState w in preConditions)
            {
                preConditionsDic.Add(w.stateKeyName, w.value);
            }
        }
        
        if (afterEffects != null)
        {
            foreach (WorldState w in afterEffects)
            {
                afterEffectsDic.Add(w.stateKeyName, w.value);
            }
        }

        inventory = GetComponent<Agent>().inventory;
        agentBeliefs = GetComponent<Agent>().beliefs;

    }

    public bool IsAchieveable()
    {
        return true;
    }

    public bool IsAchieveableGiven(Dictionary<string, int> conditions)
    {
        foreach(KeyValuePair<string, int> kvp in preConditionsDic)
        {
            if (!conditions.ContainsKey(kvp.Key))
                return false;
        }
        return true;
    }

    public abstract bool PrePerform();
    public abstract bool PostPerform();

}
