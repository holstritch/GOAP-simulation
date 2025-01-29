using UnityEngine;
using UnityEngine.Serialization;

namespace Actions
{
    public class GoToWaitingRoomAction : GameAction
    {
        [SerializeField] private WorldState patientWaitingState; // change to just grabbing from action post petform
         // change this to agent belief
         [SerializeField] private WorldState atWaitingRoomState;

        public override bool PrePerform()
        {
            return true;
        }

        public override bool PostPerform()
        {
            GameWorld.Instance.GetWorld().ModifyState(patientWaitingState.stateKeyName, 1);
            GameWorld.Instance.AddPatient(gameObject);
            agentBeliefs.ModifyState(atWaitingRoomState.stateKeyName, 1);

            return true;
        }
    
    }
}
