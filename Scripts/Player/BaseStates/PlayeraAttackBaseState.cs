using UnityEngine;
using Player.Params;
using Player.PlayerStates.Attack;

namespace Player.PlayerSkills
{
    public abstract class PlayeraAttackBaseState
    {
        protected AttackManager AttackManager;
        protected PlayeraAttackBaseState stateMachine;
        protected PlayerAttackParams data;

        public PlayeraAttackBaseState(AttackManager AttackManager, PlayeraAttackBaseState stateMachine, PlayerAttackParams data)
        {
            this.AttackManager = AttackManager;
            this.stateMachine = stateMachine;
            this.data = data;
        }
        
        public abstract void EnterState(AttackManager playerState);
    
        public abstract void UpdateState(AttackManager playerState);
    
        public abstract void OnCollisionEnter(AttackManager playerState);

        public abstract void FixUpdate();
            
        public void SwitchState(PlayeraAttackBaseState state)
        {
            AttackManager._currentState = state;
            state.EnterState(AttackManager);         
        }
    }
}