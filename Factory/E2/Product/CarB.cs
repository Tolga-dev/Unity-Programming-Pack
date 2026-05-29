using Factory.E2.Base;
using UnityEngine;

namespace Factory.E2.Product
{
    public class CarB : Car
    {
        public CarB(string carName = "", GameObject carPrefab = null)
        {
            name = carName;
            prefab = carPrefab;
        }
    }
}