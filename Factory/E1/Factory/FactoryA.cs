using Factory.E1.Base;
using Factory.E1.Products;
using UnityEngine;
using UnityEngine.Serialization;

namespace Factory.E1.Factory
{
    public class FactoryA : FactoryBase
    {
        [SerializeField] private ProductA productPrefab;

        public override IProduct GetProduct(Vector3 position)
        {
            var instance = Instantiate(productPrefab.gameObject, position, Quaternion.identity);
            var newProduct = instance.GetComponent<ProductA>();
            newProduct.Initialize();

            return newProduct;
        }
    }
}