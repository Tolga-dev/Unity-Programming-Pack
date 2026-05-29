using Player.Params;
using Player.PlayerStates.Movement;
using UnityEngine;

namespace Player.PlayerSkills.Movement
{
    public class PlayerGetInCarState : PlayerMovementBaseState
    {
        
        public PlayerGetInCarState(MovementManager movementManager, PlayerMovementBaseState stateMachine, PlayerMovementParams data)
            : base(movementManager, stateMachine, data) { }

        public PlayerMovementBaseState CurrentState { get; private set; }
        
        public override void EnterState(MovementManager playerState)
        {
          //  movementManager.playerAnimator.runtimeAnimatorController = movementManager.GetInTheCarAnims;
        }

        public override void UpdateState(MovementManager playerState)
        {

            SwitchState(movementManager.WalkState); 
            Debug.Log("Car Update State");
        }
    
        public override void OnCollisionEnter(MovementManager playerState)
        {
        }
        
        public override void FixUpdate()
        {
            
        }
         
        
        

        
    }
}