namespace Actions
{
    public class GetPatientAction : GameAction
    {
        public override bool PrePerform()
        {
            target = GameWorld.Instance.RemovePatient();
            if (target != null)
            {
                return true;
            }
            
            return false;
        }

        public override bool PostPerform()
        {
            return true;
        }
    
    }
}
