using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void EnterState(PlayerStateMachine playerState);
    
    public abstract void UpdateState(PlayerStateMachine playerState);
    
    public abstract void OnCollisionEnter(PlayerStateMachine playerState);

}
