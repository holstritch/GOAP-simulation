namespace Agents
{
    public class Nurse : Agent
    {
        private new void Start()
        {
            base.Start();
            SubGoal subGoal1 = new SubGoal("treatPatient", 1, true);
            goals.Add(subGoal1, 3);
        }
    
    }
}
