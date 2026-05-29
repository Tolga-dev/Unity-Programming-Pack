using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Solid.E1
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] public Vector3 vector3;
        [SerializeField] private IPlayerAction _currentAction;
        [SerializeField] private MoveAction _moveAction;
        [SerializeField] private JumpAction _jumpAction;

        private void Awake()
        {
            _moveAction = new MoveAction(this);
            _jumpAction = new JumpAction(this);
        }

        private void Start()
        {

            _currentAction = _moveAction;
        }

        public void SetInput(Vector3 newInput)
        {
            vector3 = newInput;
            _currentAction = _moveAction;
            Performing();
        }
        public void SetJump()
        {
            _currentAction = _jumpAction;
            Performing();
        }

        public void Performing()
        {
            _currentAction.PerformAction();
        }
        
        
    }

    public interface IPlayerAction // Open closed 
    {
        
        public void PerformAction();
    }

    public class MoveAction : IPlayerAction
    {
        private PlayerMovement _playerMovement;

        public MoveAction(PlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
        }
        public void PerformAction()
        {
            Debug.Log("MoveAction");
            _playerMovement.transform.position += _playerMovement.vector3;
        }
    }
    public class JumpAction : IPlayerAction
    {
        private PlayerMovement _playerMovement;

        public JumpAction(PlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
        }
        
        public void PerformAction()
        {
            Debug.Log("JumpAction");
            var transform = _playerMovement.transform;
            var vector3 = transform.position;
            
            vector3.y = 10;
            transform.position = vector3;
        }
    }
    

}