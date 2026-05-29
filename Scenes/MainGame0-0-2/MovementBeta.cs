using System;
using BaseStates;
using Manager;
using Skills.MovementSkills.Command;
using Skills.MovementSkills.States;
using UnityEngine; 

namespace Manager
{
    public class MovementBeta : MonoBehaviour
    {
        [SerializeField] public MovementBaseState m_CurrentState;
        private  PlayerWalkState m_WalkState;
        private  PlayerRunState m_RunState;
        public  PlayerIdleState m_IdleState;
        private  MovementCommandManager CommandManager;
        
        public CharacterController controller;
        private void Start()
        {
            
            controller = GetComponent<CharacterController>();
            m_CurrentState = m_IdleState;
            m_CurrentState.EnterState(this);
            
        }

        private void Awake()
        {
            CommandManager = new MovementCommandManager();
            m_WalkState = new PlayerWalkState(this, m_CurrentState,CommandManager,controller);
            m_RunState = new PlayerRunState(this, m_CurrentState,CommandManager,controller);
            m_IdleState = new PlayerIdleState(this, m_CurrentState,CommandManager,controller);
            
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
}


namespace Skills
{
    namespace MovementSkills
    {
        namespace Command
        {

            public abstract class SubStateBaseCommand
            {
                public abstract Vector3 UpdateCommand();
                
            }

            public class LeftCommand : SubStateBaseCommand
            { 
                public override Vector3 UpdateCommand()
                {
                    Debug.Log("Left");
                    return Vector3.right;  // Vector3(-1, 0, 0)

                }
            }
        
            public class RightCommand : SubStateBaseCommand
            { 
                public override Vector3 UpdateCommand()
                {
                    Debug.Log("Right");
                    return Vector3.left; // Vector3(1, 0, 0)
                }
            }
            public class ForwardCommand : SubStateBaseCommand
            {
                public override Vector3 UpdateCommand()
                {
                    Debug.Log("Forward");

                        return Vector3.back;  // Vector3(0, 0, 1)
                }
            }
            public class BackCommand : SubStateBaseCommand
            { 
                public override Vector3 UpdateCommand( )
                {
                    Debug.Log("Back");
                    return Vector3.forward; // Vector3(0, 0, -1)
                }
            } 
            
            public class JumpCommand
            { 
                public void EnterState(ref Vector3 Velocity)
                {
                    Debug.Log("Jump Enter");

                    Velocity.y = Mathf.Sqrt(100 * 9);

                    ExitState();
                    
                }

                public void ExitState()
                {
                    Debug.Log("Jump Exit");
                }
                public void Jump(ref Vector3 velocity)
                {
                    
                }
                
            }

            public class MovementCommandManager
            {
                public readonly KeyCode Left = KeyCode.A;
                public readonly KeyCode Right = KeyCode.D;
                public readonly KeyCode Forward = KeyCode.W;
                public readonly KeyCode Back = KeyCode.S;
                public readonly KeyCode Run = KeyCode.LeftShift;
                public readonly KeyCode Jump = KeyCode.Space;
                
                
                public readonly BackCommand BackCommand = new BackCommand();
                public readonly ForwardCommand ForwardCommand = new ForwardCommand();
                public readonly LeftCommand LeftCommand = new LeftCommand();
                public readonly RightCommand RightCommand = new RightCommand();
                public readonly JumpCommand JumpCommand = new JumpCommand();
            }
            
        }
        // velocity + input farklidir bunlari yapacaz
        // kodlari rahatlatlaim
        // jump finish -> github a gitcek bu dosya
        namespace States
        {
            public class PlayerInputManager
            {
                public void Update(MovementCommandManager commandManager,ref Vector3 input)
                {
 
                    if(!Input.anyKey) input = Vector3.zero;
                    if(Input.GetKey(commandManager.Forward))
                        input.z = commandManager.ForwardCommand.UpdateCommand().z;
                    if(Input.GetKey(commandManager.Back)) 
                        input.z = commandManager.BackCommand.UpdateCommand().z;
                    if(Input.GetKey(commandManager.Left))
                        input.x = commandManager.LeftCommand.UpdateCommand().x;
                    if(Input.GetKey(commandManager.Right))
                        input.x = commandManager.RightCommand.UpdateCommand().x;
                    if (Input.GetKeyDown(commandManager.Jump))
                        commandManager.JumpCommand.EnterState(ref input);
                    
                }
                
            }
            
            public class PlayerWalkState : MovementBaseState
            {
                private readonly PlayerInputManager _playerInputManager = new PlayerInputManager();
                
                public PlayerWalkState(MovementBeta movementManager, MovementBaseState playerMovementBaseState,MovementCommandManager movementCommandManager, CharacterController characterController)
                    : base(movementManager, playerMovementBaseState,movementCommandManager,characterController)
                {
                }

                public override void EnterState(MovementBeta movementState)
                {
                    Debug.Log("Enter Walk State"); 
                }

                public override void UpdateState(MovementBeta movementState)
                {
                    Debug.Log("Update Walk State");

                    if(Input.anyKey) // no input
                        _playerInputManager.Update(CommandManager,ref PlayerInput);
                    else
                        ExitState(movementState);

                    Movement(PlayerInput);
                    
                    PlayerInput = Vector3.zero;

                }

                private static void ExitState(MovementBeta movementState)
                {
                    movementState.m_CurrentState = movementState.m_IdleState;
                }
      

            }
            public class PlayerRunState : MovementBaseState
            {
                
                private readonly PlayerInputManager _playerInputManager = new PlayerInputManager();
                private MovementBaseState _movementBaseState;
                public PlayerRunState(MovementBeta movementManager, MovementBaseState playerMovementBaseState,MovementCommandManager commandManager,CharacterController characterController)
                    : base(movementManager, playerMovementBaseState,commandManager,characterController)
                {
                    _movementBaseState = playerMovementBaseState;
                }

                public override void EnterState(MovementBeta movementState)
                {
                    Debug.Log("Enter Run State");
                }

                public override void UpdateState(MovementBeta movementState)
                {
                    Debug.Log("Update Run State");
                    if(Input.anyKey) // no input
                        _playerInputManager.Update(CommandManager,ref PlayerInput);
                    else
                        ExitState(movementState);

                    Movement(PlayerInput*2);
                    
                    PlayerInput = Vector3.zero;
                    
                }
                private static void ExitState(MovementBeta movementState)
                {
                    movementState.m_CurrentState = movementState.m_IdleState;
                }
       
            }       
            public class PlayerIdleState : MovementBaseState
            {
                public readonly MovementCommandManager CommandManager = new MovementCommandManager();
                private readonly PlayerInputManager _playerInputManager = new PlayerInputManager();
                public PlayerIdleState(MovementBeta movementManager, MovementBaseState playerMovementBaseState,MovementCommandManager movementCommandManager,CharacterController characterController)
                    : base(movementManager, playerMovementBaseState,movementCommandManager,characterController) { }

                public override void EnterState(MovementBeta movementState)
                {
                    Debug.Log("Enter IDLE State");
                }

                public override void UpdateState(MovementBeta movementState)
                {
                    Debug.Log("Update IDLE State");
                    if (PlayerInput.y < 0)
                    {
                        PlayerInput.y = -2f;
                    }

                    PlayerInput.y += -9 * Time.deltaTime; 

                    _playerInputManager.Update(CommandManager,ref PlayerInput);
                    Movement(PlayerInput);

                }

                public void ExitState(MovementBeta movementState)
                {
                    
                }
                
            }
        }
        
    } 
    
}

namespace BaseStates
{
    [Serializable]
    public abstract class MovementBaseState
    {
        private MovementBaseState _mMovementBaseState;
        private MovementBeta _mMovementManager;
        public CharacterController _characterController;
        protected Vector3 PlayerInput = new Vector3(0,0,0);
        protected readonly MovementCommandManager CommandManager;

        protected MovementBaseState(MovementBeta movementManager, MovementBaseState movementBaseState,MovementCommandManager commandManager,CharacterController characterController)
        {
            this.CommandManager = commandManager;
            this._mMovementBaseState = movementBaseState;
            this._mMovementManager = movementManager;
            this._characterController = characterController;
        }

        public abstract void EnterState(MovementBeta movementState);
        
        public abstract void UpdateState(MovementBeta movementState);

        public void SwitchState(MovementBaseState nextState)
        {  }
        protected void Movement(Vector3 moveInput)
        {
            
            _characterController.Move(moveInput * Time.deltaTime);
        }
        
    }
}

