using System.Collections.Generic;
using UnityEngine;

namespace DataLocality.E1.Scripts
{
    public class DataLocality : MonoBehaviour
    {
        // Start is called before the first frame update
        private readonly List<GameEntity> _entities = new List<GameEntity>();

        private void Start()
        {
            var component = new Component();
            _entities.Add(new GameEntity(component));
            _entities.Add(new GameEntity(component));
            _entities.Add(new GameEntity(component));
            _entities.Add(new GameEntity(component));
        
        }

        // Update is called once per frame
        private void Update()
        {
            for (var i = 0; i < 10; i++)
            {
                _entities[i].component.Update();
            }
        }
    }

    public class Component
    {
        public void Update()
        {
        
        }
    };

    public class GameEntity
    {
        public Component component;

        public GameEntity(Component component)
        {
            this.component = component;
        }

        public Component Component()
        {
            return component; 
        }

    }
}