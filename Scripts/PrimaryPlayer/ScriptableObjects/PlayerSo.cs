using UnityEngine;

namespace PrimaryPlayer.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerSo", menuName = "SO/playerSo", order = 0)]
    public class PlayerSo : ScriptableObject
    {
        // capsule
        public static float m_CapsuleHeight;
        public static Vector3 m_CapsuleCenter;

        public static Movement Movement = new Movement();
    }

    public class Movement
    {
        // Helpers
        
        // Movement
        public float PowerCrouch = 2f;
        public float PowerWalk = 5;
        public float PowerRun = 10;
        public float PowerJump = 1f;
        
        // Rot
        public float m_StationaryTurnSpeed = 180;
        public float m_MovingTurnSpeed = 360;
        
        
        public float WalkAnimationHelper = 0.5f; // in here, on run or walk animation. for forward, its value should be 
                                                        // decreased
                            
    }
    
}