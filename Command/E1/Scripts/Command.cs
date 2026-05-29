using UnityEngine;

namespace Command.E1.Scripts
{
    public class Command : ICommand
    {
        private readonly GameObject _gameObject;
        private readonly string _name;
        public Command(GameObject gameObject, string name)
        {
            _gameObject = gameObject;
            _name = name;
        }
        public void Execute()
        {
            var vector3 = _gameObject.transform.position;
            vector3.x += 0.1f;
            
            _gameObject.transform.position = vector3;
            
            Debug.Log("Execute "  + _name );
        }
    
        public void ExecuteUndo()
        {
            var vector3 = _gameObject.transform.position;
            vector3.x -= 0.1f;
            
            _gameObject.transform.position = vector3;

            Debug.Log("Undo "  + _name);
        }
    }
    
}