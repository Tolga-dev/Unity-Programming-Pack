using Player.Params;
using Player.PlayerStates.Defence;
using UnityEngine;

namespace Player.PlayerSkills
{
    public abstract class PlayerDefenceBaseState
    {
        protected DefenceManager DefenceManager;
        protected PlayerDefenceBaseState stateMachine;
        protected PlayerDefenceParams data;

        public PlayerDefenceBaseState(DefenceManager DefenceManager, PlayerDefenceBaseState stateMachine, PlayerDefenceParams data)
        {
            this.DefenceManager = DefenceManager;
            this.stateMachine = stateMachine;
            this.data = data;
        }
        
        public abstract void EnterState(DefenceManager playerState);
    
        public abstract void UpdateState(DefenceManager playerState);
    
        public abstract void ExitState(DefenceManager playerState);

        public abstract void FixUpdate();
            
        public void SwitchState(PlayerDefenceBaseState state)
        {
            DefenceManager._currentState.ExitState(DefenceManager);
            DefenceManager._currentState = state;
            state.EnterState(DefenceManager);         
        }
        
    }
}