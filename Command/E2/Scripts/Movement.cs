using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command.E2.Scripts
{
    public class Movement : MonoBehaviour
    {
        private CommandE _forwardCommand;
        private CommandE _backCommandE;
        private CommandE _idleCommandE;
        private readonly List<CommandE> _storedCommands = new List<CommandE>();

        private void Start()
        {
            _forwardCommand = new Forward();
            _backCommandE = new Back();
            _idleCommandE = new DoNothing();
        } 

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                _backCommandE.Execute(_backCommandE);
                _storedCommands.Add(_backCommandE);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                _forwardCommand.Execute(_forwardCommand);
                _storedCommands.Add(_forwardCommand);
            }
            else if (Input.GetKey(KeyCode.R))
            {
                GetUndo();
            }
            else
            {
                _idleCommandE.Execute(_idleCommandE);
            }
        }

        private void GetUndo()
        {
            try
            {
                if (_storedCommands.Count == 0)
                    return;

                _storedCommands[^1].Undo();
                _storedCommands.RemoveAt(_storedCommands.Count - 1);
            }
            catch (Exception e)
            {
                Debug.Log("Index was out of range!");
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
