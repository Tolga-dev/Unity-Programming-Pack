using System.Collections.Generic;
using UnityEngine;

namespace Player.Params
{
    [CreateAssetMenu(fileName = "Player/Attack Params", menuName = "Attack Params", order = 2)]
    public class PlayerAttackParams : ScriptableObject
    {

        public float maxCameraLookUp = 20;
        public float minCameraLookUp = -20;
        public float playerAttackShootAnimSpeed = 3;
    }
}