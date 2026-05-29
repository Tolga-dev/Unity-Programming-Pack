using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    
    public override void EnterState(PlayerStateMachine playerState)
    {
        Debug.Log("Attack Enter State");
    }

    public override void UpdateState(PlayerStateMachine playerState)
    {
        Debug.Log("Attack State");
        
    }
    
    public override void OnCollisionEnter(PlayerStateMachine playerState)
    {
    }
    
}
