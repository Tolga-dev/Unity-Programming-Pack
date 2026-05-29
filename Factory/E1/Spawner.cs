using Factory.E1.Base;
using UnityEngine;

namespace Factory.E1
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private LayerMask layerToClick;
        [SerializeField] private Vector3 offset;
        [SerializeField] FactoryBase[] factories;
        [SerializeField] FactoryBase factory;

        private void Update()
        {
            GetProductAtClick();
        }

        private void GetProductAtClick()
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                factory = factories[Random.Range(0, factories.Length)];

                if (Physics.Raycast( Camera.main.ScreenPointToRay(Input.mousePosition)
                        , out var hitInfo, Mathf.Infinity, layerToClick) && factory != null)
                {
                    factory.GetProduct(hitInfo.point + offset);
                }
            }
        }
    }
}