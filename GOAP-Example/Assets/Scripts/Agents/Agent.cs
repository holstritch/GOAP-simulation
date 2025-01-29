using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Agents
{
    public class SubGoal
    {
        public Dictionary<string, int> SubGoals;
        public bool remove;

        public SubGoal(string goalName, int value, bool isRemoving)
        {
            SubGoals = new Dictionary<string, int>();
            SubGoals.Add(goalName, value);
            remove = isRemoving;
        }
    }
    public class Agent : MonoBehaviour
    {
        public List<GameAction> actions =  new List<GameAction>();
        public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
        public GameInventory inventory = new GameInventory();
        public WorldStates beliefs = new WorldStates();
        public GameAction currentAction;
        public float distanceToCompleteWaypoint = 1f;
    
        GamePlanner _planner;
        Queue<GameAction> _actionQueue;
        SubGoal _currentGoal;
        bool _invoked = false;

        void CompleteAction()
        {
            currentAction.running = false;
            currentAction.PostPerform();
            _invoked = false;
        }

        protected void Start()
        {
            GameAction[] acts = this.GetComponents<GameAction>();

            foreach (GameAction act in acts) 
            { 
                actions.Add(act);
            }
        }

        private void LateUpdate()
        {
            HandleRunningAction();
            if (currentAction != null && currentAction.running) return;
        
            CreatePlanner();
            HandleEmptyActionQueue();
            HandleNextAction(); 
        }
    
        private void HandleRunningAction()
        {
            if (currentAction != null && currentAction.running)
            {
                // already performing a plan
                if (currentAction.agent.hasPath && currentAction.agent.remainingDistance < distanceToCompleteWaypoint) // navmesh 
                {
                    if (!_invoked)
                    {
                        Invoke("CompleteAction", currentAction.duration);
                        _invoked = true;
                    }
                }
            }
        }

        private void CreatePlanner()
        {
            if (_planner == null || _actionQueue == null)
            {
                // no current plan, so create planner
                _planner = new GamePlanner();
            
                var sortedGoals = from entry in goals orderby entry.Value descending select entry;

                foreach (KeyValuePair<SubGoal, int> subGoal in sortedGoals)
                {
                    // create plan from most important goal
                    _actionQueue = _planner.Plan(actions, subGoal.Key.SubGoals, beliefs);
                    if (_actionQueue != null)
                    {
                        _currentGoal = subGoal.Key;
                        break;
                    }
                }
            }
        }

        private void HandleEmptyActionQueue()
        {
            if (_actionQueue != null && _actionQueue.Count == 0)
            {
                // have run out of actions
                if (_currentGoal.remove)
                {
                    // if goal is a removable goal
                    goals.Remove(_currentGoal);
                }
                _planner = null; // triggers creating a planner on next loop
            }
        }

        private void HandleNextAction()
        {
            if (_actionQueue != null && _actionQueue.Count > 0)
            {
                // have more actions in queue
                currentAction = _actionQueue.Dequeue();
                if (currentAction.PrePerform())
                {
                    if (currentAction.target == null && currentAction.targetTag != "")
                    {
                        currentAction.target = GameObject.FindWithTag(currentAction.targetTag);
                    }
                    if (currentAction.target != null)
                    {
                        currentAction.running = true;
                        currentAction.agent.SetDestination(currentAction.target.transform.position); // navmesh agent target
                    }
                }
                else
                {
                    _actionQueue = null; // triggers finding a new plan
                }
            }
        }
    }
}