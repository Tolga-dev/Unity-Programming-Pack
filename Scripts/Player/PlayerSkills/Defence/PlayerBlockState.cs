using Player.PlayerStates.Defence;
using UnityEngine;
using Player.Params;
using Player.PlayerInputLists;

namespace Player.PlayerSkills.Defence
{
    public class PlayerBlockState: PlayerDefenceBaseState
    {
        public PlayerBlockState(DefenceManager DefenceManager, PlayerDefenceBaseState stateMachine, PlayerDefenceParams data)
            : base(DefenceManager, stateMachine, data) { }
        
        
        public override void EnterState(DefenceManager playerState)
        {
            DefenceManager.Movementdata.WalkVelocity /= 6;
            DefenceManager.Movementdata.RunVelocity /= 6;
        }

        public override void UpdateState(DefenceManager playerState)
        {
            Debug.Log("Block update State");
            
        }
    
        public override void ExitState(DefenceManager playerState)
        {
            DefenceManager.Movementdata.WalkVelocity *= 6;
            DefenceManager.Movementdata.RunVelocity *= 6;
        }
        public override void FixUpdate()
        {
            
        }

    }
}