using UnityEngine;

namespace Player.Params
{
    [CreateAssetMenu(fileName = "Player/Movement Params", menuName = "Movement Params", order = 0)]
    public class PlayerMovementParams : ScriptableObject
    {
        [Header("Gravity")]
        public float Gravity = -9.81f;
        public float FallGravityMult;
        public float QuickFallGravityMult;
        
        [Header("Drag")]
        public float DragAmount; //drag in air
        public float FrictionAmount; //drag on ground

        [Header("Other Physics")]
        [Range(0, 0.5f)] public float CoyoteTime; //grace time to Jump after the player has fallen off a platformer
        
        [Header("Horizontal")]
        public float WalkVelocity = 1f;
        public float RunVelocity = 4f;
        public float RunAccel;
        public float RunDeccel;
        public float VelPower;
        [Range(0, 1)] public float AccelInAir;
        [Range(0, 1)] public float DeccelInAir;
        [Space(5)]
        [Range(.5f, 2f)] public float AccelPower;
        [Range(.5f, 2f)] public float StopPower;
        [Range(.5f, 2f)] public float TurnPower;

        [Header("Jump")]
        public float JumpForce = 3;
        public bool CanJump;
        public bool isGrounded;
        [Range(0, 1)] public float JumpCutMultiplier;
        [Space(10)]
        [Range(0, 0.5f)] public float JumpBufferTime; //time after pressing the jump button where if the requirements are met a jump will be automatically performed
        public float groundDistance = 0.4f;
        public float HighJumpParam; 
        [Header("Health")] public float health = 100;

    }
}