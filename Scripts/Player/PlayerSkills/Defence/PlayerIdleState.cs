using Player.PlayerStates.Defence;
using UnityEngine;
using Player.Params;
using Player.PlayerInputLists;

namespace Player.PlayerSkills.Defence
{
    public class PlayerIdleState: PlayerDefenceBaseState
    {
        public PlayerIdleState(DefenceManager DefenceManager, PlayerDefenceBaseState stateMachine, PlayerDefenceParams data)
            : base(DefenceManager, stateMachine, data) { }
        
        
        public override void EnterState(DefenceManager playerState)
        {
             
        }

        public override void UpdateState(DefenceManager playerState)
        {
            Debug.Log("Idle State");
        
            
        }
    
        public override void ExitState(DefenceManager playerState)
        {
        }
        public override void FixUpdate()
        {
            
        }

    }
}