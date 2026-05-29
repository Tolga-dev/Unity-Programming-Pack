using Player.PlayerStates.Attack;
using UnityEngine;
using Player.Params;
using Player.PlayerInputLists;

namespace Player.PlayerSkills.Attack
{
    public class PlayerAttackIdleState: PlayeraAttackBaseState
    {
        public PlayerAttackIdleState(AttackManager AttackManager, PlayeraAttackBaseState stateMachine, PlayerAttackParams data)
            : base(AttackManager, stateMachine, data) { }
        
        
        public override void EnterState(AttackManager playerState)
        {
             
        }

        public override void UpdateState(AttackManager playerState)
        {
            Debug.Log("AttackIdle State");

        }
    
        public override void OnCollisionEnter(AttackManager playerState)
        {
        }
        public override void FixUpdate()
        {
            
        }

    }
}