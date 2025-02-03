using UnityEngine;

namespace Actions
{
    public class GoToCubicleToTreatPatient : GameAction
    {
        [SerializeField] private WorldState _treatingPatientState;
        [SerializeField] private WorldState _freeCubicleState;


        public override bool PrePerform()
        {
            target = inventory.FindItemWithTag("Cubicle");
            if (target == null)
            {
                return false;
            }

            return true;
        }

        public override bool PostPerform()
        {
            GameWorld.Instance.GetWorld().ModifyState(_treatingPatientState.stateKeyName, 1);
            GameWorld.Instance.AddCubicle(target);
            inventory.RemoveItem(target);
            GameWorld.Instance.GetWorld().ModifyState(_freeCubicleState.stateKeyName, 1);
            return true;
        }
    
    }
}
