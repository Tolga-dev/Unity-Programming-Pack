using UnityEngine;

namespace Factory.E2.Base
{
    public enum CarModel
    {
        A,
        B
    }
    public abstract class Car
    {
        protected CarModel carModel;
        protected string name;
        public GameObject prefab;
    }
}