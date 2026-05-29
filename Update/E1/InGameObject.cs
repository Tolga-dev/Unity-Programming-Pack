using System;
using UnityEngine;

namespace Update.E1
{
    public class InGameObject : Object
    {
        private GameController _gameController;

        public InGameObject(GameController gameController) : base(gameController)
        {
            _gameController = gameController;
        }
    }
}