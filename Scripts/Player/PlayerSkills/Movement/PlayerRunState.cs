using Player.PlayerStates.Movement;
using UnityEngine;
using Player.Params;
using Player.PlayerInputLists;

namespace Player.PlayerSkills.Movement
{
    public class PlayerRunState: PlayerMovementBaseState
    {
        public PlayerRunState(MovementManager movementManager, PlayerMovementBaseState stateMachine, PlayerMovementParams data)
            : base(movementManager, stateMachine, data) { }
        
        
        public override void EnterState(MovementManager playerState)
        {
             
        }

        public override void UpdateState(MovementManager playerState)
        {
            Debug.Log("Run Update State");
            
            playerState.Run();
            if (!(Input.GetKey(Inputs.Run) && (Input.GetKey(Inputs.Forward) || Input.GetKey(Inputs.Right) || Input.GetKey(Inputs.Left) || Input.GetKey(Inputs.Back)))) //change state to runnig, when moveInput detected
            {
                SwitchState(playerState.WalkState);    
                
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