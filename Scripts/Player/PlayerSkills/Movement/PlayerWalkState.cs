using Player.PlayerStates.Movement;
using UnityEngine;
using Player.Params;
using Player.PlayerInputLists;

namespace Player.PlayerSkills.Movement
{
    public class PlayerWalkState: PlayerMovementBaseState
    {
        public PlayerWalkState(MovementManager movementManager, PlayerMovementBaseState stateMachine, PlayerMovementParams data)
            : base(movementManager, stateMachine, data) { }
        
        
        public override void EnterState(MovementManager playerState)
        {
             
        }

        public override void UpdateState(MovementManager playerState)
        {
            Debug.Log("Walk State");
            movementManager.Walk();
            
            if(movementManager.MoveInput.x == 0 && movementManager.MoveInput.z == 0) //change state to runnig, when moveInput detected
            {
                
                SwitchState(playerState.IdleState);
            }

            if (Input.GetKey(Inputs.Run) && (Input.GetKey(Inputs.Forward) || Input.GetKey(Inputs.Right) || Input.GetKey(Inputs.Left) || Input.GetKey(Inputs.Back)))
            {
                SwitchState(playerState.RunState);
            }
            
        }
    
        public override void OnCollisionEnter(MovementManager playerState)
        {
        }
        public override void FixUpdate()
        {
            
        }
        

    }
}