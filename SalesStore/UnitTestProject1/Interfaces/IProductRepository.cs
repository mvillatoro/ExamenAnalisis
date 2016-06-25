using System.Collections.Generic;

namespace UnitTestProject1.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product GetProduct(int productId);

    }
}