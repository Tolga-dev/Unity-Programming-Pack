using System;
using PrimaryPlayer.PlayerComponentManagers.Movement;
using UnityEngine;

namespace PrimaryPlayer.BaseStates
{
    [Serializable]
    public abstract class PlayerMovementBaseState
    {
        public MovementManager MovementManager;
        public PlayerMovementBaseState BaseState;
        
        protected PlayerMovementBaseState(
            MovementManager movementManager,
            PlayerMovementBaseState playerMovementBaseState
            )
        {
            this.MovementManager = movementManager;
            this.BaseState = playerMovementBaseState;
        }
        
        public abstract void EnterState(MovementManager movementState);
        
        public abstract void UpdateState(MovementManager movementState);
        public abstract void ExitState(MovementManager movementState);

    }
    
    
}