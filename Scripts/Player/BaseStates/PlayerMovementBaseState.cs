using Player.Params;
using Player.PlayerStates.Movement;
using UnityEngine;

namespace Player.PlayerSkills
{
    public abstract class PlayerMovementBaseState
    {
        protected MovementManager movementManager;
        protected PlayerMovementBaseState stateMachine;
        protected PlayerMovementParams data;

        public PlayerMovementBaseState(MovementManager movementManager, PlayerMovementBaseState stateMachine, PlayerMovementParams data)
        {
            this.movementManager = movementManager;
            this.stateMachine = stateMachine;
            this.data = data;
        }
        
        public abstract void EnterState(MovementManager playerState);
    
        public abstract void UpdateState(MovementManager playerState);
    
        public abstract void OnCollisionEnter(MovementManager playerState);

        public abstract void FixUpdate();
            
        public void SwitchState(PlayerMovementBaseState state)
        {
            movementManager._currentState = state;
            state.EnterState(movementManager);         
        }
        
    }
    
}