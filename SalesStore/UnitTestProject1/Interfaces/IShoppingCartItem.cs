using System.Collections.Generic;
using UnitTestProject1.Entites;

namespace UnitTestProject1.Interfaces
{
    public interface IShoppingCartItem
    {
        List<ShoppingCartItem> GetByCart(int cartId);
    }
}