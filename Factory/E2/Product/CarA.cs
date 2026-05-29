using Factory.E2.Base;
using UnityEngine;

namespace Factory.E2.Product
{
    public class CarA : Car
    {
        public CarA(string carName = "", GameObject carPrefab = null)
        {
            name = carName;
            prefab = carPrefab;
        }
    }
}