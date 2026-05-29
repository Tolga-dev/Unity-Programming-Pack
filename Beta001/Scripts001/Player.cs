using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseStates;
using JetBrains.Annotations;
using Managers;
using Skills.AttackSkills;
using Skills.DefenceSkills;
using Skills.MovementSkills;
using UnityEngine;



public class Player : MonoBehaviour
{
    public GameObject _player;
   
    void Start()
    {
        _player = new GameObject();

        _player.AddComponent<AttackManager>();
        _player.AddComponent<DefenceManager>();
        _player.AddComponent<MovementManager>();

    }
    
}


namespace Skills
{
    namespace MovementSkills
    {
        public abstract class SubStateBaseCommand
        {
            public abstract void UpdateState();

        }

        public class LeftCommand : SubStateBaseCommand
        { 
            public override void UpdateState()
            {
                Debug.Log("Left");
            }
        }
        
        public class RightCommand : SubStateBaseCommand
        { 
            public override void UpdateState()
            {
                Debug.Log("Right");
            }
        }
        public class ForwardCommand : SubStateBaseCommand
        {
            public override void UpdateState()
            {
                Debug.Log("Forward");
            }
        }
        public class BackCommand : SubStateBaseCommand
        { 
            public override void UpdateState()
            {
                Debug.Log("Back");
            }
        } 
        public class JumpCommand
        { 
            public void EnterState()
            {
                Debug.Log("Jump Enter");
                ExitState();
            }
            public void ExitState()
            {
                Debug.Log("Jump Exit");
            }
        }
        
        public class MovementCommandManager
        {
            public KeyCode Left = KeyCode.A;
            public KeyCode Right = KeyCode.D;
            public KeyCode Forward = KeyCode.W;
            public KeyCode Back = KeyCode.S;
            public KeyCode Run = KeyCode.LeftShift;
            public KeyCode Jump = KeyCode.Space;
            
            public BackCommand backCommand = new BackCommand();
            public ForwardCommand forwardCommand = new ForwardCommand();
            public LeftCommand leftCommand = new LeftCommand();
            public RightCommand rightCommand = new RightCommand();
            public JumpCommand jumpCommand = new JumpCommand();
            
        }
        
        public class PlayerWalkState : PlayerMovementBaseState
        {
            public MovementCommandManager commandManager = new MovementCommandManager(); 
            public PlayerWalkState(MovementManager movementManager, PlayerMovementBaseState playerMovementBaseState)
                : base(movementManager, playerMovementBaseState)
            {
            }

            public override void EnterState(MovementManager movementState)
            {
                Debug.Log("Enter Walk State"); 
            }

            public override void UpdateState(MovementManager movementState)
            {
                Debug.Log("Update Walk State");

                if(Input.GetKey(commandManager.Forward))
                    commandManager.forwardCommand.UpdateState();
                if(Input.GetKey(commandManager.Back))
                    commandManager.backCommand.UpdateState();
                if(Input.GetKey(commandManager.Left))
                    commandManager.leftCommand.UpdateState();
                if(Input.GetKey(commandManager.Right))
                    commandManager.rightCommand.UpdateState();
                if (Input.GetKey(commandManager.Jump))
                    commandManager.jumpCommand.EnterState();
                if(!Input.anyKey) // no input
                    ExitState(movementState);
                
            }

            private static void ExitState(MovementManager movementState)
            {
                movementState.m_CurrentState = movementState.m_IdleState;
            }

        }
        public class PlayerRunState : PlayerMovementBaseState
        {

            public MovementCommandManager commandManager = new MovementCommandManager(); 

            public PlayerRunState(MovementManager movementManager, PlayerMovementBaseState playerMovementBaseState)
                : base(movementManager, playerMovementBaseState)
            {
            }

            public override void EnterState(MovementManager movementState)
            {
                Debug.Log("Enter Run State");
            }

            public override void UpdateState(MovementManager movementState)
            {
                Debug.Log("Update Run State");
                if(Input.GetKey(commandManager.Forward))
                    commandManager.forwardCommand.UpdateState();
                if(Input.GetKey(commandManager.Back))
                    commandManager.backCommand.UpdateState();
                if(Input.GetKey(commandManager.Left))
                    commandManager.leftCommand.UpdateState();
                if(Input.GetKey(commandManager.Right))
                    commandManager.rightCommand.UpdateState();
                if (Input.GetKey(commandManager.Jump))
                    commandManager.jumpCommand.EnterState();
                if(!Input.anyKey) // no input
                    ExitState(movementState);
            }
            private static void ExitState(MovementManager movementState)
            {
                movementState.m_CurrentState = movementState.m_IdleState;
            }
        }       
        public class PlayerIdleState : PlayerMovementBaseState
        {
            public PlayerIdleState(MovementManager movementManager, PlayerMovementBaseState playerMovementBaseState)
                : base(movementManager, playerMovementBaseState) { }

            public override void EnterState(MovementManager movementState)
            {
                Debug.Log("Enter IDLE State");
            }

            public override void UpdateState(MovementManager movementState)
            {
                Debug.Log("Update IDLE State");
            }

            public void ExitState(MovementManager movementState)
            {
                
            }
        }
    }

    namespace AttackSkills
    {
        public class PlayerShootState: PlayerAttackBaseState
        {
            public PlayerShootState(AttackManager attackManager, PlayerAttackBaseState stateMachine)
                : base(attackManager, stateMachine) { }

            public override void EnterState(AttackManager playerState)
            {
                Debug.Log("Shoot Enter State");
            }

            public override void UpdateState(AttackManager playerState)
            {
                Debug.Log("Shoot Update State");
            }
    
        }
    }

    namespace DefenceSkills
    {
        public class PlayerBlockState : PlayerDefenceBaseState
        {
            public PlayerBlockState(DefenceManager defenceManager, PlayerDefenceBaseState stateMachine)
                : base(defenceManager, stateMachine) { }

            public override void EnterState(DefenceManager playerState)
            {
                Debug.Log("Block Enter State");
            }

            public override void UpdateState(DefenceManager playerState)
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
        [SerializeField] public PlayerMovementBaseState m_CurrentState;
        private  PlayerWalkState m_WalkState;
        private  PlayerRunState m_RunState;
        public  PlayerIdleState m_IdleState;
        public  MovementCommandManager CommandManager;
        private void Start()
        {
            m_CurrentState = m_IdleState;
            m_CurrentState.EnterState(this);
            
        }

        private void Awake()
        {
            CommandManager = new MovementCommandManager();
            m_WalkState = new PlayerWalkState(this, m_CurrentState);
            m_RunState = new PlayerRunState(this, m_CurrentState);
            m_IdleState = new PlayerIdleState(this, m_CurrentState);
            
        }

        private void Update()
        {
            
            m_CurrentState.UpdateState(this);

            if (Input.GetKey(CommandManager.Right) || Input.GetKey(CommandManager.Left) || Input.GetKey(CommandManager.Forward) || Input.GetKey(CommandManager.Back))
                m_CurrentState = m_WalkState;
            if (Input.GetKey(CommandManager.Run))
                m_CurrentState = m_RunState;
            
        }
    }    
    
    public class AttackManager : MonoBehaviour
    {
        [SerializeField] private PlayerAttackBaseState m_CurrentState;
        private  PlayerShootState m_ShootState;
        
        private void Start()
        {
            m_CurrentState = m_ShootState;
            m_CurrentState.EnterState(this);
        }

        private void Awake()
        {
            m_ShootState = new PlayerShootState(this, m_CurrentState);
            
        }

        private void Update()
        {
            if(Input.GetKey(KeyCode.Mouse0))
                m_CurrentState.UpdateState(this);

        }
    }

    public class DefenceManager : MonoBehaviour
    {
        [SerializeField] private PlayerDefenceBaseState m_CurrentState;
        private  PlayerBlockState m_BlockState;

        private void Start()
        {
            m_CurrentState = m_BlockState;
            m_CurrentState.EnterState(this);
        }

        private void Awake()
        {
            m_BlockState = new PlayerBlockState(this, m_CurrentState);
            
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

        public abstract void EnterState(MovementManager movementState);
        
        public abstract void UpdateState(MovementManager movementState);

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

        public abstract void EnterState(AttackManager attackState);
        
        public abstract void UpdateState(AttackManager attackState);

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

        public abstract void EnterState(DefenceManager defenceState);
        
        public abstract void UpdateState(DefenceManager defenceState);

        public void SwitchState(PlayerDefenceBaseState nextState)
        {
            
        }
    }
    
}
