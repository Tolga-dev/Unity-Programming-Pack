using UnityEngine;

namespace Solid.E1
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Vector3 input;
        

        
        public Vector3 GetInput()
        {
            input.x = Input.GetAxis("Horizontal");
            input.z = Input.GetAxis("Vertical");
            return input;
        }

        public bool IsJump() => Input.GetKeyDown(KeyCode.Space);
    }
}