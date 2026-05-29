using UnityEngine;

namespace PrimaryPlayer.ScriptableObjects
{
    
    public enum ChestTypes
    {
        None,
        SmallChest,
        BigChest,
        RareChest,
        AlienChest
    }

    [CreateAssetMenu(fileName = "ChestSo", menuName = "SO/ChestSO", order = 0)]
    public class Chests : ScriptableObject
    {
        // we will not be here like we did in itemSo i guess
        
    }

}