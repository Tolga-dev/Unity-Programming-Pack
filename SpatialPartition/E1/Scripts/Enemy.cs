using UnityEngine;

namespace SpatialPartition.E1.Scripts
{
    public class Enemy : Soldier
    {
        private Vector3 currentTarget;
        private Vector3 oldPos;
        private float mapWidth;
        private Grid _grid;
    
    
        public Enemy(GameObject SoldierObj, float mapWidth, Grid grid)
        {

            this.SoldierTransform = SoldierObj.transform;
            this.mapWidth = mapWidth;
            this._grid = grid;
            this.WalkSpeed = 5f;
        
            grid.Add(this);

            oldPos = SoldierTransform.position;
        
            GetNewTarget();
        
        }

        public override void Move()
        {
            SoldierTransform.Translate(Vector3.forward * Time.deltaTime * WalkSpeed);
            _grid.Move(this, oldPos);

            oldPos = SoldierTransform.position;
            if ((SoldierTransform.position - currentTarget).magnitude < 1f)
            {
                GetNewTarget();
            }
        }

        void GetNewTarget()
        {
            currentTarget = new Vector3(Random.Range(0f, mapWidth), 0.5f, Random.Range(0f, mapWidth));

            SoldierTransform.rotation = Quaternion.LookRotation(currentTarget - SoldierTransform.position);
        }
    }
}
