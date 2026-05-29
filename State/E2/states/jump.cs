using State.E2.entity;
using State.E2.states.stateMachine;
using UnityEngine;

namespace State.E2.states
{
    public class jump : IState
    {
        private Entity _entity;
        public jump(Entity entity)
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
            Debug.Log("Update Jump State");

            if (Input.GetKey(KeyCode.B))
                _entity.StateMachine.ToState(_entity.StateMachine.Walk);

        }

        public void Exit()
        {
            // code that runs when we exit the state
            //Debug.Log("Exiting Idle State");
        }  
    
    }
}