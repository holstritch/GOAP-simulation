using Agents;
using UnityEngine;
using UnityEngine.Serialization;

namespace Actions
{
    public class GetPatientAction : GameAction
    {
        [SerializeField] private WorldState _freeCubicleState;
        [SerializeField] private WorldState _patientWaitingState;

        private GameObject _freeCubicle;
        public override bool PrePerform()
        {
            // gets patient
            target = GameWorld.Instance.RemovePatient();
            if (target == null)
            {
                return false;
            }

            // tries to get cubicle
            _freeCubicle = GameWorld.Instance.RemoveCubicle();
            if (_freeCubicle != null)
            {
                inventory.AddItem(_freeCubicle);
            }
            else // release patient if there's no free cubicle
            {
                GameWorld.Instance.AddPatient(target);
                target = null;
                return false;
            }
            
            GameWorld.Instance.GetWorld().ModifyState(_freeCubicleState.stateKeyName, -1); 
            return true;
        }

        public override bool PostPerform()
        {
            GameWorld.Instance.GetWorld().ModifyState(_patientWaitingState.stateKeyName, -1);
            if (target != null)
            {
                target.GetComponent<Agent>().inventory.AddItem(_freeCubicle); // add to inventory of patient
            }
            return true;
        }
    
    }
}
