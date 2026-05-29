using UnityEngine;

namespace Update.E1
{
    public class Object : MonoBehaviour, IUpdateable
    {
        private GameController _gameController;

        public Object(GameController gameController)
        {
            _gameController = gameController;
        }

        public virtual void OnUpdate()
        {
            Debug.Log("updated " + name);
        }
        
    }
}