namespace Actions
{
    public class GoToWaitingRoomAction : GameAction
    {
        public override bool PrePerform()
        {
            return true;
        }

        public override bool PostPerform()
        {
            GameWorld.Instance.GetWorld().ModifyState("Waiting", 1);
            GameWorld.Instance.AddPatient(gameObject);
            return true;
        }
    
    }
}
