using System;
using PrimaryPlayer.BaseStates;
using PrimaryPlayer.PlayerComponentManagers.Movement;
using UnityEngine;


namespace PrimaryPlayer.PlayerSkills.Movement
{
    public class PlayerIdleState : PlayerMovementBaseState
    {
        public PlayerIdleState(MovementManager movementManager,
            PlayerMovementBaseState playerMovementBaseState
        ) : base(movementManager,playerMovementBaseState)
        { 
            
        }

        public override void EnterState(MovementManager movementState)
        {
//            Debug.Log("Enter Idle State");
        }

        public override void UpdateState(MovementManager movementState)
        {   
          //movementState.playerController.Move(movementState.playerInputVec* movementState.powerwalk * Time.deltaTime);

           // Debug.Log("Update Idle State");
        }
        
        public override void ExitState(MovementManager movementState)
        {
           // Debug.Log("Exited Idle State");
        }

        
    }
}