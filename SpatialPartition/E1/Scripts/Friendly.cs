using UnityEngine;

namespace SpatialPartition.E1.Scripts
{
    public class Friendly : Soldier
    {
        public Friendly(GameObject soldierObj, float mapWidth)
        {
            this.SoldierTransform = soldierObj.transform;

            this.WalkSpeed = 2f;
        }

        public override void Move(Soldier soldier)
        {
            SoldierTransform.rotation = Quaternion.LookRotation(soldier.SoldierTransform.position - SoldierTransform.position);
        
            SoldierTransform.Translate(Vector3.forward * Time.deltaTime * WalkSpeed);
            base.Move();
        }
    }
}
