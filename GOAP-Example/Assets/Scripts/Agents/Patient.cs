using UnityEngine;

namespace Agents
{
    public class Patient : Agent
    {
        private new void Start()
        {
            base.Start();
            if (worldState != null)
            {
                SubGoal subGoal1 = new SubGoal(worldState.stateKeyName, worldState.value, true);
                goals.Add(subGoal1, 3);
            }
            else
            {
                Debug.LogError("⚠️ World State scriptable object is missing, assign it to the NPC Agent.");
            }

        }
    }
}
