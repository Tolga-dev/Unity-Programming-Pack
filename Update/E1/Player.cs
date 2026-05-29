using UnityEngine;

namespace Update.E1
{
    public class Player : Object
    {
        private GameController _gameController;
        private Vector3 _input;
        
        public Player(GameController gameController) : base(gameController)
        {
            _gameController = gameController;
        }
        
        
        public override void OnUpdate()
        {
            base.OnUpdate();
            
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            
            _input.x = horizontal;
            _input.z = vertical;
            
            
            transform.position += _input;
            
        }
        
        
    }
}