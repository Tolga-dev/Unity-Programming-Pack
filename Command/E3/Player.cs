using UnityEngine;
using UnityEngine.Serialization;

namespace Command.E3
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        public Rigidbody myRigidBody;
    
        private Transform _myTransform;
        private float _flVertical;
        private float _flHorizontal;
        private readonly float _flRotateMultiplier = 3;
    
        void Awake()
        {
            _myTransform = transform;
            myRigidBody = GetComponent<Rigidbody>();
        }
        public void RunForward()
        {
            _flHorizontal = 0;
            _flVertical = 1f;
            myRigidBody.velocity = _myTransform.forward * _flVertical;
        }

        public void RunBack()
        {
            _flHorizontal = 0;
            _flVertical = -1f;
            myRigidBody.velocity = _myTransform.forward * _flVertical;
        }
        public void RotateRight()
        {
            _flVertical = 0;
            _flHorizontal = 1f;
            _myTransform.Rotate(0, _flHorizontal * _flRotateMultiplier, 0);
        }
    
        public void RotateLeft()
        {
            _flVertical = 0;
            _flHorizontal = -1f;
            _myTransform.Rotate(0, _flHorizontal * _flRotateMultiplier, 0);
        }

        public void Jump()
        {
            var transform1 = transform;
            var vector3 = transform1.position;
            vector3.y = 10;
            
            transform1.position = vector3;
            // Shooting
        }

    }
}
