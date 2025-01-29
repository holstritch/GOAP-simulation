using UnityEngine;

namespace Agents
{
    public class Nurse : Agent
    {
        private new void Start()
        {
            base.Start();
            SubGoal subGoal1 = new SubGoal(worldState.stateKeyName, worldState.value, true);
            goals.Add(subGoal1, 3);
        }
    }
}
