using System;
using Factory.E2.Base;
using Factory.E2.Factory;
using Factory.E2.Product;
using UnityEngine;

namespace Factory.E2
{
    public class FactoryController : MonoBehaviour
    {
        public string aCarName = "a Car Name";
        public string bCarName = "b Car Name";

        public GameObject aCarPrefab;
        public GameObject bCarPrefab;
        private void Start()
        {
            var factoryA = new ACompany(aCarName, aCarPrefab);
            
            var produceCarA = factoryA.ProduceCar();
            CreateCar(ref produceCarA);

            var factoryB = new BCompany(bCarName, bCarPrefab);
            var produceCarB = factoryB.ProduceCar();
            CreateCar(ref produceCarB);
        }

        private void CreateCar(ref Car car)
        {
            Instantiate(car.prefab);
        }
    }
}