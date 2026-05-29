using PrimaryPlayer.BaseStates;
using PrimaryPlayer.PlayerComponentManagers.Attack;
using PrimaryPlayer.PlayerComponentManagers.Movement;
using UnityEngine;

namespace PrimaryPlayer.PlayerSkills.Attack
{
    public class PlayerIdleState : PlayerAttackBase
    {
        
        public PlayerIdleState(
            AttackManager attackManager,
            MovementManager movementManager,
            PlayerAttackBase playerAttackBase
        ) : base(attackManager,movementManager,playerAttackBase)
        {
        }

        public override void EnterState(ref AttackManager attackManager)
        {
          //  Debug.Log("Idle Entered!");   
        }

        public override void UpdateState(ref AttackManager attackManager)
        {
//            Debug.Log("Idle Update!");   
            
        }

        public override void ExitState(ref AttackManager attackManager)
        {
         //   Debug.Log("Idle Exit!");   
            
        }

    }
}