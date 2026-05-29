using Factory.E1.Products;
using UnityEngine;

namespace Factory.E1.Base
{
    public abstract class FactoryBase : MonoBehaviour
    {
        public abstract IProduct GetProduct(Vector3 position);
        
    }
}