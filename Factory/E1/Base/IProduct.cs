namespace Factory.E1.Base
{
    public interface IProduct
    {
        public string ProductName { get; set; }

        public void Initialize();
    }
}