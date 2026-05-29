using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Command.E1.Scripts
{
    public class Object : MonoBehaviour
    {
        private ICommand _command;
        private readonly List<ICommand> _commandsBuffer = new List<ICommand>();
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (Physics.Raycast(Camera.main!.ScreenPointToRay(Input.mousePosition), out var hit))
                {
                    var o = hit.collider.gameObject;
                    AddCommandToBuffer(new Command(o, o.name));
                }
            }
            else if (Input.GetKey(KeyCode.R))
            {
                Undo();
            }
            
        }

        private void Undo()
        {
            UndoRoutine();
        }

        private void UndoRoutine()
        {
            if (_commandsBuffer.Count == 0)
                return;
            
            var command = _commandsBuffer[^1];

            command.ExecuteUndo();
            _commandsBuffer.Remove(command);

        }

        private void AddCommandToBuffer(ICommand command)
        {
            Debug.Log("Command added");
            command.Execute();
            _commandsBuffer.Add(command);

        }

    }
}