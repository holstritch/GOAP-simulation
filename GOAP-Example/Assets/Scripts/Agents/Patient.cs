using UnityEngine;

namespace Agents
{
    public class Patient : Agent
    {
        [SerializeField] private WorldState isWaitingState;
        [SerializeField] private WorldState isTreatedState;
        private new void Start()
        {
            base.Start();
            if (isWaitingState != null)
            {
                SubGoal subGoal1 = new SubGoal(isWaitingState.stateKeyName, 1, true);
                goals.Add(subGoal1, 3);
            }
            else
            {
                Debug.LogError("⚠️ isWaitingState scriptable object is missing, assign it to the NPC Agent.");
            }
            
            if (isTreatedState != null)
            {
                SubGoal subGoal1 = new SubGoal(isTreatedState.stateKeyName, 1, true);
                goals.Add(subGoal1, 5);
            }
            else
            {
                Debug.LogError("⚠️ isTreatedState scriptable object is missing, assign it to the NPC Agent.");
            }

        }
    }
}
