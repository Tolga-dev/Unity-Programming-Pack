using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : PlayerBaseState
{
    
    public override void EnterState(PlayerStateMachine playerState)
    {
        Debug.Log("Movement Enter State");
    }

    public override void UpdateState(PlayerStateMachine playerState)
    {
        Debug.Log("Movement State");
    }
    
    public override void OnCollisionEnter(PlayerStateMachine playerState)
    {
    }

}
