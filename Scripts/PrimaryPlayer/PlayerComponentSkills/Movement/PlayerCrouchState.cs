using PrimaryPlayer.BaseStates;
using PrimaryPlayer.PlayerComponentManagers.Movement;
using UnityEngine;

namespace PrimaryPlayer.PlayerSkills.Movement
{
    public class PlayerCrouchState : PlayerMovementBaseState
    {
        public PlayerCrouchState(MovementManager movementManager,
            PlayerMovementBaseState playerMovementBaseState
        ) : base(movementManager,playerMovementBaseState)
        { 
            
        }

        public override void EnterState(MovementManager movementState)
        {
            Debug.Log("Enter Crouch State");
            UpdateState(movementState);
        }

        public override void UpdateState(MovementManager movementState)
        {   
            movementState.playerController.Move(movementState.GroundMovement * movementState.PowerCrouch * Time.deltaTime); //  playermovement

            
            Debug.Log("Update Crouch State");
        }
        
        public override void ExitState(MovementManager movementState)
        {
            Debug.Log("Exited Crouch State");
        }

        
    }
}