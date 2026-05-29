using System.Collections.Generic;
using UnityEngine;

namespace SpatialPartition.E1.Scripts
{
    public class _GameController : MonoBehaviour
    {
        public GameObject friendObj;

        public GameObject enemyObj;

        public Transform enemyParent;
        public Transform friendlyParent;
    
        List<Soldier> _enemySoldiers = new List<Soldier>();
        List<Soldier> _friendlySoldiers = new List<Soldier>();
    
        List<Soldier> _closestEnemies = new List<Soldier>();

        private float mapWidth = 50f;
        private int cellSize = 10;

        private int numberOfSoldiers = 100;

        private Grid _grid;
    
        void Start()
        {
            _grid = new Grid((int)mapWidth, cellSize);

            for (int i = 0; i < numberOfSoldiers; i++)
            {
                Vector3 randomPos = new Vector3(Random.Range(0f, mapWidth), 0.5f, Random.Range(0f, mapWidth));
            
                GameObject enemy = Instantiate(enemyObj,randomPos,Quaternion.identity) as GameObject;
            
                _enemySoldiers.Add(new Enemy(enemy,mapWidth,_grid));

                enemy.transform.parent = enemyParent;
            
                randomPos = new Vector3(Random.Range(0f, mapWidth), 0.5f, Random.Range(0f, mapWidth));

                GameObject friend = Instantiate(friendObj,randomPos,Quaternion.identity) as GameObject;
            
                _friendlySoldiers.Add(new Friendly(friend,mapWidth));

                friend.transform.parent = enemyParent;

            }


        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < _enemySoldiers.Count; i++)
            {
                _enemySoldiers[i].Move();
            }
            _closestEnemies.Clear();

            for (int i = 0; i < _friendlySoldiers.Count; i++)
            {
                Soldier closestEnemy = _grid.FindClosest(_friendlySoldiers[i]);
                if (closestEnemy != null)
                {
                    _closestEnemies.Add(closestEnemy);
                    _friendlySoldiers[i].Move(closestEnemy);
                }
            }
        
        
        
        
        }
    }
}
