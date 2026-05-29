using Factory.E1.Base;
using UnityEngine;

namespace Factory.E1.Products
{
    public class ProductB : MonoBehaviour, IProduct
    {
        [SerializeField]private string productName= "Product B";
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