using System.Collections.Generic;

namespace UnitTestProject1.Interfaces
{
    public interface IShoppingCartRepository
    {
        List<ShoppingCart> GetAll();
        List<ShoppingCart> GetPending();
    }
}