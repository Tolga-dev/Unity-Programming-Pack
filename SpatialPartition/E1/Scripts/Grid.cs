using UnityEngine;

namespace SpatialPartition.E1.Scripts
{
    public class Grid : MonoBehaviour
    {
        private int CellSize;
        private Soldier[,] cells;
    
        public Grid(int mapWidth, int cellSize)
        {
            this.CellSize = cellSize;

            int numberOfCells = mapWidth / cellSize;

            cells = new Soldier[numberOfCells, numberOfCells];
        }

        public void Add(Soldier soldier)
        {
            int cell_x = (int)(soldier.SoldierTransform.transform.position.x / CellSize);
            int cell_z = (int)(soldier.SoldierTransform.transform.position.z / CellSize);

            soldier.PrevSoldier = null;
            soldier.NextSoldier = cells[cell_x, cell_z];
            cells[cell_x, cell_z] = soldier;

            if (soldier.NextSoldier != null)
            {
                soldier.NextSoldier.PrevSoldier = soldier;
            }
        }

        public void Move(Soldier soldier, Vector3 oldPos)
        {
            int OldCell_x = (int)(oldPos.x / CellSize);
            int OldCell_z = (int)(oldPos.z / CellSize);

            int Cell_x = (int)(soldier.SoldierTransform.position.x / CellSize);
            int Cell_z = (int)(soldier.SoldierTransform.position.z / CellSize);
        
            if(OldCell_x == Cell_x && OldCell_z == Cell_z) return;
        
            if (soldier.PrevSoldier != null)
            {
                soldier.PrevSoldier.NextSoldier = soldier.NextSoldier;
            }

            if (soldier.NextSoldier != null)
            {
                soldier.NextSoldier.PrevSoldier = soldier.PrevSoldier;
            }

            if (cells[OldCell_x, OldCell_z] == soldier)
            {
                cells[OldCell_x, OldCell_z] = soldier.NextSoldier;
            }

            Add(soldier);
        }

        public Soldier FindClosest(Soldier soldier)
        {
            int cellX = (int)(soldier.SoldierTransform.position.x / CellSize);
            int cellZ = (int)(soldier.SoldierTransform.position.z / CellSize);

            Soldier enemy = cells[cellX, cellZ];

            Soldier closestSoldier = null;

            float bestDistSqr = Mathf.Infinity;

            while (enemy != null)
            {
                float distSqr = (enemy.SoldierTransform.position - soldier.SoldierTransform.position).sqrMagnitude;

                if (distSqr < bestDistSqr)
                {
                    bestDistSqr = distSqr;

                    closestSoldier = enemy;
                }

                enemy = enemy.NextSoldier;
            }

            return closestSoldier;
        }
    }
}
