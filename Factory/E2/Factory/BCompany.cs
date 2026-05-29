using Factory.E2.Base;
using Factory.E2.Product;
using UnityEngine;

namespace Factory.E2.Factory
{
    public class BCompany : CarFactory
    {
        private readonly string _name;
        private readonly GameObject _prefab;
        public BCompany(string name, GameObject carPrefab)
        {
            _name = name;
            _prefab = carPrefab;
        }
        public override Car ProduceCar()
        {
            var produced = new CarB(_name, _prefab);
            return produced;
        }
    }
}