using UnityEngine;

namespace Agents
{
    public class Nurse : Agent
    {
        [SerializeField] private WorldState treatPatientState;

        private new void Start()
        {
            base.Start();
            SubGoal subGoal1 = new SubGoal(treatPatientState.stateKeyName, 1, true);
            goals.Add(subGoal1, 3);
        }
    }
}
