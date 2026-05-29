using System;
using PrimaryPlayer.BaseStates;
using PrimaryPlayer.PlayerComponentManagers.Movement;
using UnityEngine;
using UnityEngine.PlayerLoop;


namespace PrimaryPlayer.PlayerSkills.Movement
{
    public class PlayerRunState: PlayerMovementBaseState
    {
        public PlayerRunState(MovementManager movementManager,
            PlayerMovementBaseState playerMovementBaseState
        ) : base(movementManager,playerMovementBaseState)
        { 
            
        }

        public override void EnterState(MovementManager movementState)
        {
            // Debug.Log("Enter Run State");
            movementState.isOnWalkAnimation = false;

            UpdateState(movementState);
        }
        

        public override void UpdateState(MovementManager movementState)
        {
            Debug.Log("Update Run State");
            //use Transform.InverseTransformPoint if the vector represents a position in space rather than a direction.
            movementState.playerController.Move(movementState.GroundMovement * movementState.PowerRun * Time.deltaTime); //  playermovement
            
            /*if (!movementState.InputManager.CheckSpecificInput())
                ExitState(movementState);
            
            if (!Input.GetKey(movementState.movementCommandManager.Keycode.Run))
                ExitState(movementState);
                */
            
          //  movementState.playerController.Move(movementState.playerInputVec* 2 * movementState.powerwalk * Time.deltaTime);
            
        }   
        
        public override void ExitState(MovementManager movementState)
        {
            movementState.CurrentState = movementState.WalkState;
            Debug.Log("Exited Run State");
        }

    }
}