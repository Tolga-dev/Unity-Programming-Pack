using System;
using State.E2.entity;

namespace State.E2.states.stateMachine
{
    public class stateMachine
    {
        public IState CurrentState;

        public walk Walk;
        public idle Idle;
        public jump Jump;

        public event Action<IState> stateChanged;

        public stateMachine(Entity entity)
        {
            this.Walk = new walk(entity);
            this.Jump = new jump(entity);
            this.Idle = new idle(entity);
        }

        public void Init(IState state)
        {
            CurrentState = state;
            state.Enter();
        
            stateChanged?.Invoke(state);
        }

        public void ToState(IState next)
        {
            CurrentState.Exit();
            CurrentState = next;
            CurrentState.Enter();
        
            stateChanged?.Invoke(next);
        }

        public void Update()
        {
            if (CurrentState != null)
            {
                CurrentState.Update();
            }
        }

    }
}