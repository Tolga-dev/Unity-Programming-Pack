using Factory.E1.Base;
using Factory.E1.Products;
using UnityEngine;

namespace Factory.E1.Factory
{
    public class FactoryB : FactoryBase
    {
        [SerializeField] private ProductB productPrefab;

        public override IProduct GetProduct(Vector3 position)
        {
            var instance = Instantiate(productPrefab.gameObject, position, Quaternion.identity);
            var newProduct = instance.GetComponent<ProductB>();
            newProduct.Initialize();

            return newProduct;
        }   
    }
}