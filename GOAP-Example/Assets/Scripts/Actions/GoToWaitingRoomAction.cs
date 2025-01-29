using UnityEngine;

namespace Actions
{
    public class GoToWaitingRoomAction : GameAction
    {
        [SerializeField] protected WorldState postPerformState; // patient waiting

        public override bool PrePerform()
        {
            return true;
        }

        public override bool PostPerform()
        {
            GameWorld.Instance.GetWorld().ModifyState(postPerformState.stateKeyName, 1);
            GameWorld.Instance.AddPatient(gameObject);
            return true;
        }
    
    }
}
