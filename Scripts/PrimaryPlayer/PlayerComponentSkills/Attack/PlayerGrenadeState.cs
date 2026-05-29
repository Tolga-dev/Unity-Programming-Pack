using Npc;
using PrimaryPlayer.BaseStates;
using PrimaryPlayer.PlayerComponentManagers.Attack;
using PrimaryPlayer.PlayerComponentManagers.Movement;
using UnityEngine;

namespace PrimaryPlayer.PlayerComponentSkills.Attack
{
    public class PlayerGrenadeState : PlayerAttackBase
    {

        public PlayerGrenadeState(
            AttackManager attackManager,
            MovementManager movementManager,
            PlayerAttackBase playerAttackBase
        ) : base(attackManager, movementManager, playerAttackBase)
        {

        }

        public override void EnterState(ref AttackManager attackManager)
        {
        }

        public override void UpdateState(ref AttackManager attackManager)
        {
        
            if (UnityEngine.Input.GetKeyDown(KeyCode.B))
            {
                attackManager.playerAnimator.SetBool("OnGrenade", true);
            }
            
            if(UnityEngine.Input.GetKeyUp(KeyCode.B)) 
            {
                attackManager.playerAnimator.SetBool("OnGrenade", false);
            }
            

        }

        public override void ExitState(ref AttackManager attackManager)
        {
            Debug.Log("Rifle Exit!");
        }
        
    }
}