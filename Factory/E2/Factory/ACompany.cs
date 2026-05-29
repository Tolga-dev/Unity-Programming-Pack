using Factory.E2.Base;
using Factory.E2.Product;
using UnityEngine;

namespace Factory.E2.Factory
{
    public class ACompany : CarFactory
    {
        private readonly string _name;
        private readonly GameObject _prefab;
        public ACompany(string name, GameObject carPrefab)
        {
            _name = name;
            _prefab = carPrefab;
        }
        public override Car ProduceCar()
        {
            var produced = new CarA(_name, _prefab);
            return produced;
        }
    }
}