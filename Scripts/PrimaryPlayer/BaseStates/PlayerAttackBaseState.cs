using System;
using PrimaryPlayer.PlayerComponentManagers.Attack;
using PrimaryPlayer.PlayerComponentManagers.Movement;
using UnityEngine;

namespace PrimaryPlayer.BaseStates
{
    
    [Serializable]
    public abstract class PlayerAttackBase
    {
        public MovementManager _MovementManager;
        public AttackManager _AttackManager;
        public PlayerAttackBase _PlayerAttackBase;

        public PlayerAttackBase(
             AttackManager attackManager,
             MovementManager movementManager,
             PlayerAttackBase playerAttackBase
        )
        {
            this._AttackManager = attackManager;
            this._MovementManager = movementManager;
            this._PlayerAttackBase = _PlayerAttackBase;
        }
        
        public abstract void EnterState(ref AttackManager attackManager);
        
        public abstract void UpdateState(ref AttackManager attackManager);
        
        public abstract void ExitState(ref AttackManager attackManager);

        
    }
}