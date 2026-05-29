using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player002 : MonoBehaviour
{
/*
    public GameObject obj;

    void Start()
    {
        obj = new GameObject();
        obj.AddComponent<Managers.AttackManager>();
        obj.AddComponent<Managers.DefenceManager>();
        obj.AddComponent<Managers.MovementManager>();

    }
    */
}
/*
namespace Skills
{
    namespace MovementSkills
    {
        public class PlayerWalkState : BaseStates.PlayerMovementBaseState
        {
            public PlayerWalkState(Managers.MovementManager movementManager, BaseStates.PlayerMovementBaseState playerMovementBaseState)
                : base(movementManager, playerMovementBaseState) { }

            public override void EnterState(Managers.MovementManager movementState)
            {
                Debug.Log("Enter Walk State");
            }

            public override void UpdateState(Managers.MovementManager movementState)
            {
                Debug.Log("Update Walk State");
            }
        }
    }

    namespace AttackSkills
    {
        public class PlayerShootState: BaseStates.PlayerAttackBaseState
        {
            public PlayerShootState(Managers.AttackManager attackManager, BaseStates.PlayerAttackBaseState stateMachine)
                : base(attackManager, stateMachine) { }

            public override void EnterState(Managers.AttackManager playerState)
            {
                Debug.Log("Shoot Enter State");
            }

            public override void UpdateState(Managers.AttackManager playerState)
            {
                Debug.Log("Shoot Update State");
            }
    
        }
    }

    namespace DefenceSkills
    {
        public class PlayerBlockState : BaseStates.PlayerDefenceBaseState
        {
            public PlayerBlockState(Managers.DefenceManager defenceManager, BaseStates.PlayerDefenceBaseState stateMachine)
                : base(defenceManager, stateMachine) { }

            public override void EnterState(Managers.DefenceManager playerState)
            {
                Debug.Log("Block Enter State");
            }

            public override void UpdateState(Managers.DefenceManager playerState)
            {
                Debug.Log("Block Update State");
            }
        }
    }
    
}
namespace Managers
{
    public class MovementManager : MonoBehaviour
    {
        [SerializeField] private BaseStates.PlayerMovementBaseState m_CurrentState;
        private  Skills.MovementSkills.PlayerWalkState m_WalkState;

        private void Start()
        {
            m_CurrentState = m_WalkState;
            m_CurrentState.EnterState(this);
        }

        private void Awake()
        {
            m_WalkState = new Skills.MovementSkills.PlayerWalkState(this, m_CurrentState);
            
        }

        private void Update()
        {
            if(Input.GetKey(KeyCode.W))
                m_CurrentState.UpdateState(this);
        }
    }    
    
    public class AttackManager : MonoBehaviour
    {
        [SerializeField] private BaseStates.PlayerAttackBaseState m_CurrentState;
        private  Skills.AttackSkills.PlayerShootState m_ShootState;

        private void Start()
        {
            m_CurrentState = m_ShootState;
            m_CurrentState.EnterState(this);
        }

        private void Awake()
        {
            m_ShootState = new Skills.AttackSkills.PlayerShootState(this, m_CurrentState);
            
        }

        private void Update()
        {
            if(Input.GetKey(KeyCode.Mouse0))
                m_CurrentState.UpdateState(this);

        }
    }

    public class DefenceManager : MonoBehaviour
    {
        [SerializeField] private BaseStates.PlayerDefenceBaseState m_CurrentState;
        private  Skills.DefenceSkills.PlayerBlockState m_BlockState;

        private void Start()
        {
            m_CurrentState = m_BlockState;
            m_CurrentState.EnterState(this);
        }

        private void Awake()
        {
            m_BlockState = new Skills.DefenceSkills.PlayerBlockState(this, m_CurrentState);
            
        }

        private void Update()
        {
            if(Input.GetKey(KeyCode.Mouse1))
                m_CurrentState.UpdateState(this);

        }
    }
}

namespace BaseStates
{
    public abstract class PlayerMovementBaseState
    {
        private PlayerMovementBaseState m_PlayerMovementBaseState;
        private Managers.MovementManager m_MovementManager;
        protected PlayerMovementBaseState(Managers.MovementManager movementManager, PlayerMovementBaseState playerMovementBaseState)
        {
            this.m_PlayerMovementBaseState = playerMovementBaseState;
            this.m_MovementManager = movementManager;
        }

        public abstract void EnterState(Managers.MovementManager movementState);
        
        public abstract void UpdateState(Managers.MovementManager movementState);

        public void SwitchState(PlayerMovementBaseState nextState)
        {
            
        }
    } 
    public abstract class PlayerAttackBaseState
    {
        private PlayerAttackBaseState m_PlayerAttackBaseState;
        private Managers.AttackManager m_AttackManager;
        protected PlayerAttackBaseState(Managers.AttackManager attackManager, PlayerAttackBaseState playerAttackBaseState)
        {
            this.m_PlayerAttackBaseState = playerAttackBaseState;
            this.m_AttackManager = attackManager;
        }

        public abstract void EnterState(Managers.AttackManager attackState);
        
        public abstract void UpdateState(Managers.AttackManager attackState);

        public void SwitchState(PlayerAttackBaseState nextState)
        {
            
        }
    } 
    public abstract class PlayerDefenceBaseState
    {
        private PlayerDefenceBaseState m_PlayerDefenceBaseState;
        private Managers.DefenceManager m_DefenceManager;
        protected PlayerDefenceBaseState(Managers.DefenceManager defenceManager, PlayerDefenceBaseState playerDefenceBaseState)
        {
            this.m_PlayerDefenceBaseState = playerDefenceBaseState;
            this.m_DefenceManager = defenceManager;
        }

        public abstract void EnterState(Managers.DefenceManager defenceState);
        
        public abstract void UpdateState(Managers.DefenceManager defenceState);

        public void SwitchState(PlayerDefenceBaseState nextState)
        {
            
        }
    }
    
}*/