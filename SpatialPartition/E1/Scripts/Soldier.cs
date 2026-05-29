using UnityEngine;

namespace SpatialPartition.E1.Scripts
{
    public class Soldier
    {
        public Transform SoldierTransform;

        public float WalkSpeed;

        public Soldier NextSoldier;
        public Soldier PrevSoldier;
    
        public virtual void Move() {}
        public virtual void Move(Soldier soldier) {}
    
    
    

    }
}

