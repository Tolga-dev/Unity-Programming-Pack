using System;
using UnityEngine;

namespace ObjectPool.E1
{
    public class Spawner : MonoBehaviour
    {
        public Pool pool;
        public string poolName = "Spawn";
        public int size = 10;
        public GameObject objectSpawned;

        [SerializeField] private LayerMask layerToClick;
        [SerializeField] private Vector3 offset;
        private void Start()
        {
            pool.CreatePool<GameObject>(poolName, size);

            for (var i = 0; i < 10; i++)
            {
                var prefab = Instantiate(objectSpawned);
                prefab.SetActive(false);
                pool.AddObjectToPool(poolName, prefab, i);
            }
        }

        public void Update()
        {

            if (Input.GetMouseButtonDown(0))
            {

                if (Physics.Raycast( Camera.main.ScreenPointToRay(Input.mousePosition)
                        , out var hitInfo, Mathf.Infinity, layerToClick))
                {
                    GetObjectFromPool(hitInfo.point + offset);
                }
            }

        }

        private void GetObjectFromPool(Vector3 position)
        {
            var prefab = pool.GetObjectFromPool<GameObject>(poolName);

            prefab.transform.position = position;
            var rb = prefab.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            prefab.SetActive(true);
        }
    }
}