using UnityEngine;

namespace Actions
{
    public class GetTreated : GameAction
    {
        [SerializeField] private WorldState _isTreatedState;

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
            GameWorld.Instance.GetWorld().ModifyState(_isTreatedState.stateKeyName, 1);
            inventory.RemoveItem(target);
            return true;
        }
    }
}
