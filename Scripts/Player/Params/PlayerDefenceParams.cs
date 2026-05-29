using UnityEngine;

namespace Player.Params
{
    [CreateAssetMenu(fileName = "Player/Defence Params", menuName = "Defence Params", order = 1)]
    public class PlayerDefenceParams : ScriptableObject
    {
        [Header("Armour")]
        public float armor = 100;
    }
}