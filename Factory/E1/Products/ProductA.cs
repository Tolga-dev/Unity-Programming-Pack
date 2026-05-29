using Factory.E1.Base;
using UnityEngine;

namespace Factory.E1.Products
{
    public class ProductA : MonoBehaviour, IProduct
    {
        [SerializeField]private string productName= "Product A";
        public string ProductName
        {
            get => productName;
            set => productName = value;
        }
        
        public void Initialize()
        {
            Debug.Log(ProductName + " is initialized");
        }
        
    }
}