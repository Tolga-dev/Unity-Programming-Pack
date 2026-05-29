using System;
using System.Collections.Generic;
using PrimaryPlayer.GameEngine;
using UnityEngine;

namespace PrimaryPlayer.PlayerComponentManagers.Input
{
    public class Input : MonoBehaviour
    {
        private KeyCodeManager _keyCodeManager = new KeyCodeManager();
        public Dictionary<int, Action> States = new Dictionary<int, Action>();
        
        private void Start()
        {
            States.Add(12, WalkingState);
            
        }

        private void WalkingState()
        {
            
        }

        private void RunningState()
        {
            
        }
    }
}