using State.E2.entity;
using State.E2.states.stateMachine;
using UnityEngine;

namespace State.E2.states
{
    public class idle : IState
    {
        private Entity _entity;
        public idle(Entity entity)
        {
            this._entity = entity;
        }
        public void Enter()
        {
            // code that runs when we first enter the state
            Debug.Log("Entering Idle State");
        }

        // per-frame logic, include condition to transition to a new state
        public void Update()
        {
            Debug.Log("Update Idle State");
            if (Input.GetKey(KeyCode.A))
                _entity.StateMachine.ToState(_entity.StateMachine.Jump);
        
        }

        public void Exit()
        {
            // code that runs when we exit the state
            Debug.Log("Exiting Idle State");
        }  
    
    }
}