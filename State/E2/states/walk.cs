using State.E2.entity;
using State.E2.states.stateMachine;
using UnityEngine;

namespace State.E2.states
{
    public class walk : IState
    {
        private Entity _entity;
        public walk(Entity entity)
        {
            this._entity = entity;
        }
        public void Enter()
        {
            // code that runs when we first enter the state
            //Debug.Log("Entering Idle State");
        }

        // per-frame logic, include condition to transition to a new state
        public void Update()
        {
            Debug.Log("Update Walk State");

        }

        public void Exit()
        {
            // code that runs when we exit the state
            //Debug.Log("Exiting Idle State");
        }  
    }
}