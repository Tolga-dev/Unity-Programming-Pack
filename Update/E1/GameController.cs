using System;
using System.Collections.Generic;
using UnityEngine;

namespace Update.E1
{
    [Serializable]
    public class GameController : MonoBehaviour
    {
        [SerializeField] private readonly List<IUpdateable> _updateableObjects = new List<IUpdateable>();
        public bool isGamePaused;

        public Player player;
        public InGameObject obj;
        private void Awake()
        {
            AddUpdateAbleObject(player);
            AddUpdateAbleObject(obj);
        }

        public void Update()
        {
            if (!isGamePaused)
            {
                foreach (var updateable in _updateableObjects)
                {
                    updateable.OnUpdate();
                }
            }
            
            if (Input.GetKeyDown(KeyCode.P))
                isGamePaused = true;
            else if (Input.GetKeyDown(KeyCode.O))
                isGamePaused = false;
            
        }

        private void AddUpdateAbleObject( IUpdateable updateable)
        {
            _updateableObjects.Add(updateable);
        }
        
    }
}