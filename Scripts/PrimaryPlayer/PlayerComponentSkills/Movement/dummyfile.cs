namespace PrimaryPlayer.PlayerSkills.Movement
{
    public class dummyfile
    {
        /*
         * 
        [SerializeField] 
        public PlayerMovementBaseState m_CurrentState;
        public  PlayerWalkState m_WalkState;
        public  PlayerRunState m_RunState;
        public  PlayerIdleState m_IdleState;
        public  PlayerJumpState JumpState;
        public  MovementCommandManager CommandManager;

        public PlayerManager.PlayerManager PlayerManager;
        
        public Inputx ImInputx;
        public inputy ImInputy;
        // for input
        private float DeadTimeSensitivity = 0.001f;
        public float minivme_yf = 0;
        public float max_y = 0;
        public float minivme_xf = 0;
        public float max_x = 0;
        
        public Vector3 playerMovement = new Vector3(0,0,0);
        public Vector3 playerInputVec = new Vector3(0, 0, 0);
        public CharacterController playerController;
        
        // for jump
        public Transform groundCheckTransform; // gotten from Left_Foot pos
        public float groundCheckTransformRadiusDistance = 0.2f; // sphere radius 
        private bool _mIsGrounded; 
        private bool _playerEnteredJump; 
        public float _jumpPower = 1f;
        //
        public LayerMask Jump_Layer; // acceptable layers -> ground wall etc
        [SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
        const float k_Half = 0.5f;
        [Range(1f, 4f)][SerializeField] float m_GravityMultiplier = 2f;

        
        //crouch
        private bool _playerEnteredCrouch; 
        bool m_Crouching;

        
        public float CapsuleHeight = 1;
        public float CapsuleCenter = 1;

		 // walk - run
		private Vector3 GroundMovement;                   // the world-relative desired move direction, calculated from the camForward and user input.
		public float m_MoveSpeedMultiplier = 1f;
		private Vector3 m_Move;
		public float powerwalk = 100;
		public float input_lower = 100;
		public float mulpliyerofFormwardAmout = 100;
		// rotate
		[SerializeField] float m_StationaryTurnSpeed = 180;
		[SerializeField] float m_MovingTurnSpeed = 360;
		float m_TurnAmount;
		private float _mForwardAmount;
		
		// camera
		public Transform m_Cam;                  // A reference to the main camera in the scenes transform
		private Vector3 m_CamForward;             // The current forward direction of the camera
		
        /*
        // trying
        [SerializeField] float m_JumpPower = 12f;
        [SerializeField] float m_AnimSpeedMultiplier = 1f;
        [SerializeField] float m_GroundCheckDistance = 0.1f;


        float m_OrigGroundCheckDistance;
        Vector3 m_GroundNormal;
        float m_CapsuleHeight;
        Vector3 m_CapsuleCenter;
        CapsuleCollider m_Capsule;
        private float animationcontrol;
        private bool crouch;
        *//*
        public bool playerCanJump = true;
        public int powerjump = 5;

        private void Start()
        {
            if (m_Cam == null)
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.",
                    gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!

            }

            PlayerManager = GetComponent<PlayerManager.PlayerManager>();
            //m_CurrentState = m_IdleState;
            //m_CurrentState.EnterState(this);

            // INPUT
            
            ImInputx = new Inputx();
            ImInputy = new inputy();
            // INPUT GENERATION
            minivme_xf = ImInputx.minivme_x;
            max_x = ImInputx.ivme_x;
            minivme_yf = ImInputy.minivme_yf;
            max_y = ImInputy.ivme_yf;
            
            
        }
        */
        /*
        private void Awake()
        {
            
            CommandManager = new MovementCommandManager();
            m_WalkState = new PlayerWalkState(this, m_CurrentState);
            m_RunState = new PlayerRunState(this, m_CurrentState);
            m_IdleState = new PlayerIdleState(this, m_CurrentState);
            JumpState = new PlayerJumpState(this, m_CurrentState);
        }
        *//*
        
        private void FixedUpdate()
        {
            // read inputs 
            StartCoroutine(ImInputx.SleepForDead()); // sleep for 10 ms
	        playerInputVec.x = ImInputx.GetAxis_x(); // x   a = -1 , d = 1 
	        playerInputVec.z = ImInputy.GetAxis_y(); // z -> not y  my mistake w = 1 s = -1
	        _playerEnteredJump = Input.GetKeyDown(KeyCode.Space);
	        _playerEnteredCrouch = Input.GetKeyDown(KeyCode.LeftShift);
	        m_Crouching= Input.GetKey(KeyCode.C);
	        
//			Debug.Log(playerInputVec.x + " "  + playerInputVec.z + " " + Input.GetAxis("Horizontal") + " " + Input.GetAxis("Vertical"));
	         
            float h = playerInputVec.x;
            float v = playerInputVec.z;
            
            
            CheckGroundStatus();
            Debug.Log(_mIsGrounded);
            
            
            // calculate move direction to pass to character
            // calculate camera relative direction to move:
            // camera will be our compass, we will take direction according camara that looks 
            if (Time.deltaTime > 0)
            {
	            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
	            m_Move = playerInputVec.z*m_CamForward + playerInputVec.x*m_Cam.right;
            }
            
            
            if (!_mIsGrounded) // for y movement, gravity
            {
	            //playerMovement.y += -9.81f * Time.deltaTime * m_GravityMultiplier;
	            playerMovement.y += Physics.gravity.y * Time.deltaTime;
	            
            }
            Debug.Log(playerMovement.y);
            playerController.Move(playerMovement * Time.deltaTime); //  playermovement just for gravity 
            
            // real movement
            Move(m_Move);
        }*/
        /*
        private void Move(Vector3 move)
        {
	        
	        // convert the world relative moveInput vector into a local-relative
	        // turn amount and forward amount required to head in the desired
	        // direction.

	        //When normalized, a vector keeps the same direction but its length is 1.0.
	        //Note that this function will change the current vector. If you want to keep the current vector unchanged, use normalized variable.
	        //If this vector is too small to be normalized it will be set to zero.
	        if (move.magnitude > 1f) move.Normalize();
	        //use Transform.InverseTransformPoint if the vector represents a position in space rather than a direction.
	        playerController.Move(GroundMovement * powerwalk * Time.deltaTime); //  playermovement

	        move = transform.InverseTransformDirection(move);
	        // to find out direction 
	        move = Vector3.ProjectOnPlane(move, Vector3.up);
	        // tanjant animation math
	        m_TurnAmount = Mathf.Atan2(move.x, move.z);
	        _mForwardAmount = move.z * input_lower;
	        _mForwardAmount *= Input.GetKey(KeyCode.LeftShift) ? mulpliyerofFormwardAmout : 1;

	        ApplyExtraTurnRotation();
	        
	        // jump events
	        if (_mIsGrounded)
	        {
		        Debug.Log("Can Jump!");
		        if (_playerEnteredJump && !_playerEnteredCrouch && PlayerManager.animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
		        {
			        playerMovement.y = 0; 
			        playerMovement.y = _jumpPower; // 1 meters 
			        PlayerManager.animator.applyRootMotion = false;
		        }
		    }
	        else
	        {
		        Debug.Log("Cannot Jump!");
	        }
	        
	        // is grounded situations
	        // control and velocity handling is different when grounded and airborne:
	        // send input and other state parameters to the animator
	        UpdateAnimator();
	        
        }

		private void UpdateAnimator()
		{
			var runCycle =
				Mathf.Repeat(
					PlayerManager.animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
			var jumpLeg = (runCycle < k_Half ? 1 : -1) * _mForwardAmount;

			
			//  Debug.Log("1 "+m_TurnAmount + " " + m_ForwardAmount);
			// update the animator parameters
			PlayerManager.animator.SetFloat("Forward", _mForwardAmount, 0.1f, Time.deltaTime);
			PlayerManager.animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);	
			PlayerManager.animator.SetBool("OnGround", _mIsGrounded);
			PlayerManager.animator.SetBool("Crouch", m_Crouching);
			
			//Debug.Log(playerController.velocity.y);

			if (_mIsGrounded)
			{ 
				PlayerManager.animator.SetFloat("JumpLeg", jumpLeg);
			}
			else
			{
				PlayerManager.animator.SetFloat("Jump", playerMovement.y);
			}
			
		}
		
		void ApplyExtraTurnRotation()
		{
			// turning
			var turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, _mForwardAmount);
			transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
		}
		
		public void OnAnimatorMove()
		{
			// we implement this function to override the default root motion.
			// this allows us to modify the positional speed before it's applied.
			if (_mIsGrounded && Time.deltaTime > 0)
			{
				/*Debug.Log(PlayerManager.animator.deltaPosition
				          + " " + PlayerManager.animator.deltaPosition * m_MoveSpeedMultiplier
				          + " " +  PlayerManager.animator.deltaPosition * m_MoveSpeedMultiplier / Time.deltaTime
				          );*/
            /*
				GroundMovement = (PlayerManager.animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;
				
			}
		}
		 
            
		private void CheckGroundStatus()
		{
			// helper to visualise the ground check ray in the scene view
			var position = groundCheckTransform.position;
			
			// 0.1f is a small offset to start the ray from inside the character
			_mIsGrounded =  Physics.CheckSphere(position, groundCheckTransformRadiusDistance, Jump_Layer);
			
	//		Debug.DrawLine(groundCheckTransform.position, -Vector3.one * groundDistance, Color.black);
	//		Debug.DrawLine(groundCheckTransform.position, Vector3.one * groundDistance, Color.black);
	
			//ScaleCapsuleForJumping();
		}

		private void ScaleCapsuleForJumping()
		{
	
			playerController.center = playerController.center / CapsuleCenter;
			playerController.height = playerController.height / CapsuleHeight;
			
		}*/
		
        /*
        private void Update()
        {
            
            if (Input.GetKey(CommandManager.Right) || Input.GetKey(CommandManager.Left) ||
                Input.GetKey(CommandManager.Forward) || Input.GetKey(CommandManager.Back))
            {
                if (Input.GetKey(CommandManager.Run))
                    m_CurrentState = m_RunState;
                else
                    m_CurrentState = m_WalkState;
            }
            
            playerCanJump =  Physics.CheckSphere(groundCheck.position, groundDistance, layers);
            if (playerCanJump)
            {
               // SleepForDead();
                playerInputVec.x = Input.GetAxis("Horizontal");
                playerInputVec.z = Input.GetAxis("Vertical");
 
                playerInputVec = transform.TransformDirection(playerInputVec);
                playerInputVec.Normalize(); 

 
                if (Input.GetKeyDown(CommandManager.Jump))
                {
                    m_CurrentState = JumpState;
                    m_CurrentState.EnterState(this);
                }

            }
            else
            {
                playerMovement.y += -9.81f * Time.deltaTime * 2;
            }
            
            
            playerController.Move(playerMovement * Time.deltaTime);
            //movementState.playerController.Move(movementState.playerInputVec* movementState.powerwalk * Time.deltaTime);

            m_CurrentState.UpdateState(this);
            
        }
 


        IEnumerator SleepForDead()
        {
            yield return new WaitForSeconds(DeadTimeSensitivity);
        }
        */
        
    
        
         
    }
}