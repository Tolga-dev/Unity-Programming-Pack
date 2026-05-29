using PrimaryPlayer.BaseStates;
using PrimaryPlayer.PlayerComponentManagers.Attack;
using PrimaryPlayer.PlayerComponentManagers.Movement;
using UnityEngine;

namespace PrimaryPlayer.PlayerComponentSkills.Attack
{
    public class PlayerRifleState : PlayerAttackBase
    {
        
        public PlayerRifleState(
              AttackManager attackManager,
             MovementManager movementManager,
             PlayerAttackBase playerAttackBase
        ) : base(attackManager,movementManager,playerAttackBase)
        {
            
        }

        public override void EnterState(ref AttackManager attackManager)
        {
            Debug.Log("Rifle Entered!");   
        }

        public override void UpdateState(ref AttackManager attackManager)
        {
            SelectTargetCheck(ref attackManager);
            OnFireCheck(ref attackManager); 

        }
        
        public override void ExitState(ref AttackManager attackManager)
        {
            Debug.Log("Rifle Exit!");   
            
        }

        private void SelectTargetCheck(ref AttackManager attackManager)
        {
            
            if (attackManager.InputManager.SelectingTargetDown())
            { 
                attackManager.InputActionOnSelectingTarget();
            }
            else if (attackManager.InputManager.SelectingTargetUp())
            {
                attackManager.InputActionUpSelectingTarget();
            } 
            
        }

        private void OnFireCheck(ref AttackManager attackManager)
        {
            if (attackManager.InputManager.OnFireDown())
            {
                attackManager.InputActionOnFireDown();
               
            }
            else if (attackManager.InputManager.OnFireUp())
            {
                attackManager.InputActionOnFireUp();
            }
        }

       

    }
}