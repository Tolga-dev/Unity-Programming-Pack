using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefenceState : PlayerBaseState
{

    public override void EnterState(PlayerStateMachine playerState)
    {
        Debug.Log("Defence Enter State");
    }

    public override void UpdateState(PlayerStateMachine playerState)
    {
        Debug.Log("Defence State");
        
        
    }

    
    public override void OnCollisionEnter(PlayerStateMachine playerState)
    {
    }

}
