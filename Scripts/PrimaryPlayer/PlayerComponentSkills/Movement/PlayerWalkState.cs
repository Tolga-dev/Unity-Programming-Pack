using System;
using PrimaryPlayer.BaseStates;
using PrimaryPlayer.PlayerComponentManagers.Movement;
using UnityEngine;

namespace PrimaryPlayer.PlayerSkills.Movement
{
    
    public class PlayerWalkState: PlayerMovementBaseState
    {
        public PlayerWalkState(
            MovementManager movementManager,
            PlayerMovementBaseState playerMovementBaseState
        ) : base(movementManager,playerMovementBaseState)
        { 
            
        }

        public override void EnterState(MovementManager movementState)
        {
//            Debug.Log("Enter Walk State");
            movementState.isOnWalkAnimation = true;
            UpdateState(movementState);
        }

        public override void UpdateState(MovementManager movementState)
        {
            if(!Input.anyKey) // no input
                ExitState(movementState); 
//          movementState.playerController.Move(movementState.playerInputVec* movementState.powerwalk * Time.deltaTime);
            movementState.playerController.Move(movementState.GroundMovement * movementState.PowerWalk * Time.deltaTime); //  playermovement
            
            

//            Debug.Log("Update Walk State");
        }
        
        public override void ExitState(MovementManager movementState)
        {
            movementState.CurrentState = movementState.IdleState;
            movementState.isOnWalkAnimation = false;
            
//            Debug.Log("Exited Walk State");
        }


    }
}