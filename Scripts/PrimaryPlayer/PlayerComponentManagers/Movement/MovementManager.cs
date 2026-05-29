using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Input_Update_Movement;
using PrimaryPlayer.GameEngine;
using PrimaryPlayer.BaseStates;
using PrimaryPlayer.PlayerComponentManagers.Movement;
using PrimaryPlayer.PlayerComponentManagers.Movement.User_Defined;
using PrimaryPlayer.PlayerSkills.Movement;
using Skills.MovementSkills.Command;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;
 
// ideas
// before doing general attack manager, we can change general state to current state
// so we may do something else for this situation
 
// to do list
// movement cods

	// stack in rifle movement animation
		// on target mechanism
		// chest rotation
		// punch integration
		// on target, can be layer to make unity happy, after so much time later
	
	// inventory
		// selected item can be throw with q, and spawning same like inv
	// carrying something from ground, and place it to somewhere else, throwing it somewhere else
		// holding state, slower, holding with two hand

	//* jump and falling is not acceptable
		// after jumping-crouching, players collider should be short -> while jumping not will be change but in crouching ok, maybe after time, we may change it too why not
	//* inventory saving
	//* inventory, staying behind inventory // try dont change parent 
	//* general sprint and drinking for later
	
	// chest, saving, 
	// move from inventory to chest iwc
		// first added parent
		//
	// car
	// aeroplane
	// ship 
	// npc
	// back-bag
	// armor
	// shooting gun types
	// grenades types,
	// health types,
	// potion types,
	// magic,
	// magical tools
	// story 
	// boss 
	// flying
	// swimming
	// shooting gun animation types
	// magical tools animation types
	// near to wall
	// animations 
	// weather conditions
	//encryption, decryption


// attack cods
	// target and camera rotation is terrible generally

namespace PrimaryPlayer.PlayerComponentManagers.Movement
{
    public class MovementManager : MonoBehaviour
    {
			
        [SerializeField] public PlayerMovementBaseState CurrentState;
        [SerializeField] public  PlayerWalkState WalkState;
        [SerializeField] public  PlayerRunState RunState;
        [SerializeField] public  PlayerIdleState IdleState;
        [SerializeField] public  PlayerJumpState JumpState;
        [SerializeField] public  PlayerCrouchState CrouchState;
        [SerializeField] public PlayerManager.PlayerManager PlayerManager;
        [SerializeField] public KeyCodeManager MovementKeyCodeManager;
        [SerializeField] public Input_Update_Movement.InputManager InputManager;
        [SerializeField] public CharacterController playerController;

        // for jump
        public Vector3 playerAirMovement = new Vector3(0,0,0);
        [SerializeField] public bool _mIsGrounded;
        public Transform groundCheckTransform; // gotten from Left_Foot pos
        public float groundCheckTransformRadiusDistance = 0.2f; // sphere radius 
        
        // General Jump Sets
        public LayerMask Jump_Layer; // acceptable layers -> ground wall etc
        [SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
        private const float k_Half = 0.5f;
        
        
        //Input Checker
        public Vector3 playerInputVec = new Vector3(0, 0, 0);
        
        // Check Situation
        [SerializeField] public bool _playerOnJump = false;
        [SerializeField] public bool _playerOnCrouch = false;
        [SerializeField] public bool _playerOnRun = false;
        public bool isOnWalkAnimation = true; 

		 // walk - run
		 [SerializeField] public Vector3 GroundMovementHelper;
		 [SerializeField] public Vector3 GroundMovement;                   // the world-relative desired move direction, calculated from the camForward and user input.
		 
		// Movement Animation Helper
		[SerializeField] public float TurnAmount;
		[SerializeField] public float ForwardAmount;
		
		// rotate
		[SerializeField] float m_StationaryTurnSpeed = 180;
		[SerializeField] float m_MovingTurnSpeed = 360;
		
		// camera
		public Transform m_Cam;                  // A reference to the main camera in the scenes transform
		private Vector3 m_CamForward;             // The current forward direction of the camera
         
		
		// Helpers
		public float PowerCrouch = 2f;
		public float PowerWalk = 5;
		public float PowerRun = 10;
		public float PowerJump = 1f;
		
		public float WalkAnimationHelper = 0.5f; // in here, on run or walk animation. for forward, its value should be 
												// decreased
												
		// capsule values
		private float m_CapsuleHeight;
		private Vector3 m_CapsuleCenter;
		
		
		
		//debugging
		private int frameCounter = 0;
		public double maxTimeUsed = 0;
		public double maxTimeUsedByWithOutThread = 0.000202121002075728;
        private async void Start()
        {
            if (m_Cam == null)
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.",
                    gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }
		
            PlayerManager = GetComponent<PlayerManager.PlayerManager>();
            playerController = PlayerManager.playerController;
            CurrentState = IdleState;
            CurrentState.EnterState(this);
		
            m_CapsuleHeight = playerController.height; 
            m_CapsuleCenter = playerController.center;
        }

        private void Awake()
        {
	        MovementKeyCodeManager = new KeyCodeManager();
	        InputManager = new InputManager(this);
            WalkState = new PlayerWalkState(this, CurrentState);
            RunState = new PlayerRunState(this, CurrentState);
            IdleState = new PlayerIdleState(this, CurrentState);
            JumpState = new PlayerJumpState(this, CurrentState);
            CrouchState = new PlayerCrouchState(this, CurrentState);
        }
        
        private void FixedUpdate()
        {
	        
	        
	        // calculate move direction to pass to character
	        // calculate camera relative direction to move:
	        // camera will be our compass, we will take direction according camara that looks 
	        if (Time.deltaTime > 0)
	        {
		        m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
		        GroundMovementHelper = playerInputVec.z*m_CamForward + playerInputVec.x*m_Cam.right;
	        }
	        
	        AirMovement();
	        

	        Ground_Movement_Helper();
        }

        private void Update()
        {
	        InputManager.InputUpdate();
	        InputManager.StateUpdate();
	        UpdateAnimator();
        }


        private void Ground_Movement_Helper()
        {

			// convert the world relative moveInput vector into a local-relative
	        // turn amount and forward amount required to head in the desired
	        // direction.

	        //When normalized, a vector keeps the same direction but its length is 1.0.
	        //Note that this function will change the current vector. If you want to keep the current vector unchanged, use normalized variable.
	        //If this vector is too small to be normalized it will be set to zero.
	        if (GroundMovementHelper.magnitude > 1f) GroundMovementHelper.Normalize();
	        CurrentState.EnterState(this);
			
	       
	        GroundMovementHelper = transform.InverseTransformDirection(GroundMovementHelper);
	        // to find out direction 
	        GroundMovementHelper = Vector3.ProjectOnPlane(GroundMovementHelper, Vector3.up);
	        // tanjant animation math
	        TurnAmount = Mathf.Atan2(GroundMovementHelper.x, GroundMovementHelper.z);
	        ForwardAmount = isOnWalkAnimation ? GroundMovementHelper.z*WalkAnimationHelper : GroundMovementHelper.z;
	        
	        ApplyExtraTurnRotation();
	       // ScaleCapsuleForCrouching();
	        
        }
        
        private void ScaleCapsuleForCrouching()
        {
	         
        }
        	
        
        private void UpdateAnimator()
        {
	        
	        //  Debug.Log("1 "+m_TurnAmount + " " + m_ForwardAmount);
	        // update the animator parameters
	        if (PlayerManager.attackManager.onSelectingTarget)
	        {
		        PlayerManager.animator.SetFloat("Forward", -1);
		        PlayerManager.animator.SetFloat("Turn", 0);
		        PlayerManager.animator.SetFloat("OnAimingTurn", playerInputVec.x , 0.1f, Time.deltaTime);
		        PlayerManager.animator.SetFloat("OnAimingForward", playerInputVec.z, 0.1f, Time.deltaTime);
	        }
	        else
	        {
		        PlayerManager.animator.SetFloat("Forward", ForwardAmount, 0.1f, Time.deltaTime);
		        PlayerManager.animator.SetFloat("Turn", TurnAmount, 0.1f, Time.deltaTime);
	        }
	        
			
	        //Debug.Log(playerController.velocity.y);

		    PlayerManager.animator.SetBool("OnGround", _mIsGrounded);
		    
	        if (_mIsGrounded)
	        {
			   PlayerManager.animator.SetBool("OnCrouch", _playerOnCrouch);
		        PlayerManager.animator.SetFloat("JumpLeg", JumpLeg());
	        }
	        else
		        PlayerManager.animator.SetFloat("Jump", playerAirMovement.y);
			
        }
        

        private float JumpLeg() => ((JumpLegHelper() < k_Half ? 1 : -1) * ForwardAmount);
        private float JumpLegHelper() => Mathf.Repeat(
	        PlayerManager.animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);

        private void AirMovement()
        {
	        CheckGroundStatus();
	        
	        if (!_mIsGrounded) // for y movement, gravity
	        {
		        // (Alternative) //playerMovement.y += -9.81f * Time.deltaTime * m_GravityMultiplier;
		        playerAirMovement.y += Physics.gravity.y * Time.deltaTime;
	        }
	        playerController.Move(playerAirMovement * Time.deltaTime); //  playermovement just for gravity 
	        
        }
        
 
        void ApplyExtraTurnRotation()
        {
	        // turning
	        var turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, ForwardAmount);
	        transform.Rotate(0, TurnAmount * turnSpeed * Time.deltaTime, 0);
        }
        private void CheckGroundStatus()
        {
	        // helper to visualise the ground check ray in the scene view
	        // 0.1f is a small offset to start the ray from inside the character
	        _mIsGrounded =  Physics.CheckSphere(groundCheckTransform.position, groundCheckTransformRadiusDistance,
											Jump_Layer);
//					Debug.DrawLine(groundCheckTransform.position, -Vector3.one * groundDistance, Color.black);
//					Debug.DrawLine(groundCheckTransform.position, Vector3.one * groundDistance, Color.black);
	        
        }
        
        public void OnAnimatorMove()
        {
	        // we implement this function to override the default root motion.
	        // this allows us to modify the positional speed before it's applied.
	        if (_mIsGrounded && Time.deltaTime > 0)
	        {
/*
				Debug.Log(PlayerManager.animator.deltaPosition
				+ " " + PlayerManager.animator.deltaPosition * m_MoveSpeedMultiplier
				+ " " +  PlayerManager.animator.deltaPosition * m_MoveSpeedMultiplier / Time.deltaTime
						);
*/
		        GroundMovement = (PlayerManager.animator.deltaPosition * 1) / Time.deltaTime;
				Debug.Log(GroundMovement);
				
	        }
        }
        
        
    }
}
 
namespace Input_Update_Movement
{
	public class InputManager
	{
		private MovementManager _manager;
		private KeyCodeManager _keyCodeManager; 
		public InputManager(MovementManager manager)
		{
			_manager = manager;
			_keyCodeManager = _manager.MovementKeyCodeManager;
  

		}
		
		public void StateUpdate() // used in MovementManager update
		{
			
			if (_manager._mIsGrounded)
			{
				if (!CheckForZeroInputVectors())
				{

					if (_manager._playerOnRun)
					{
						_manager.CurrentState = _manager.RunState;
						
					}
					else 
						_manager.CurrentState = _manager.WalkState;
				}
				
				if (_manager._playerOnJump)
				{
					_manager.CurrentState = _manager.JumpState;
				}
				
			}
			
		}


		public bool CheckSpecificInput()
		{
			return (
					Input.GetKey(_keyCodeManager.Keycode.Right) || 
					Input.GetKey(_keyCodeManager.Keycode.Left)  || 
					Input.GetKey(_keyCodeManager.Keycode.Forward) ||
					Input.GetKey(_keyCodeManager.Keycode.Back)
					);
		}

		public bool CheckForZeroInputVectors()
		{
			return _manager.playerInputVec.x == 0 && _manager.playerInputVec.z == 0;
		}
		
		// alttaki player input vec i diom -> vector olarak fonksionun icine yazsak?
		public void InputUpdate() // get input from user, // read inputs 
		{
			
			_manager.playerInputVec.x = MyInputHorizontal.GetAxis(); // x   a = -1 , d = 1 
			_manager.playerInputVec.z = MyInputVertical.GetAxis(); // z -> not y  my mistake w = 1 s = -1
			
			_manager._playerOnJump = Input.GetKeyDown(_keyCodeManager.Keycode.Jump);
			_manager._playerOnRun = Input.GetKey(_keyCodeManager.Keycode.Run);
			_manager._playerOnCrouch = Input.GetKey(_keyCodeManager.Keycode.Crouch);
			// Debug.Log(_manager.playerInputVec.x + " "  + _manager.playerInputVec.z + "||" + Input.GetAxis("Horizontal") + " " + Input.GetAxis("Vertical"));
		}
		 
	}
	
}

