using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAddHealth : PlayerBaseState
{

    public override void EnterState(PlayerStateMachine playerState)
    {
        Debug.Log("Health Enter State");
    }

    public override void UpdateState(PlayerStateMachine playerState)
    {
        Debug.Log("Health State");
        playerState.timePassed += Time.deltaTime;
        
        if (playerState.timePassed > 1f)
        {
            playerState.timePassed = 0;
            Debug.Log("Health is added");
        }

 
    }
    
    public override void OnCollisionEnter(PlayerStateMachine playerState)
    {
        
    }
 
    

}
