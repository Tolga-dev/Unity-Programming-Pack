using System;
using UnityEngine;

namespace Command.E3
{        

    public class ExecuteCommand : MonoBehaviour
    {
        public Player plObject;

        // all commands
        private IInputCommand _upCommand;
        private IInputCommand _downCommand;
        private IInputCommand _leftCommand;
        private IInputCommand _rightCommand;
        private IInputCommand _jumpCommand;

        private void Awake()
        {
            _upCommand = new RunUpCommand();
            _downCommand = new RunBackCommand();
            _leftCommand = new RotateLeftCommand();
            _rightCommand = new RotateRightCommand();
            _jumpCommand = new JumpCommand();
        }

        private void Update()
        {   
            if (Input.GetKey(KeyCode.UpArrow))
                _upCommand.Execute(plObject);
            else if (Input.GetKey(KeyCode.DownArrow))
                _downCommand.Execute(plObject);

            if (Input.GetKey(KeyCode.RightArrow))
                _rightCommand.Execute(plObject);
            else if (Input.GetKey(KeyCode.LeftArrow))
                _leftCommand.Execute(plObject);

            if (Input.GetKey(KeyCode.Space))
            {
                _jumpCommand.Execute(plObject);
            }
    
        }
        
    
    }
}
