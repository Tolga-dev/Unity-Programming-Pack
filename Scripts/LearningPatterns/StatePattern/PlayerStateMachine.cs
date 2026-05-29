using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerBaseState _currentState;
    public float timePassed = 0f;
    
    public PlayerAttackState AttackState = new PlayerAttackState();
    public PlayerDefenceState DefenceState = new PlayerDefenceState();
    public PlayerMovementState MovementState = new PlayerMovementState();
    public PlayerDriveState DriveState = new PlayerDriveState();
    public PlayerAddHealth AddHealth  = new PlayerAddHealth();
    
    
    // Start is called before the first frame update
    void Start()
    {
        _currentState = MovementState;
        
        _currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal") ==0 && Input.GetAxis("Vertical") == 0)
            SwitchState(AddHealth);
        else
            SwitchState(MovementState);
        
        if (Input.GetKey(KeyCode.Mouse0))
            SwitchState(AttackState);
        if (Input.GetKey(KeyCode.Mouse1))
            SwitchState(DefenceState);
        
    }

    public void SwitchState(PlayerBaseState state)
    {
        _currentState = state;
        state.UpdateState(this);         
    }


}
