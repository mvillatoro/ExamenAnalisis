using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    class ShoppingCartItem
    {
        private static int _idSequence = 0;

        public int Id;
        public int CartId;
        public int ProductId;
        public int Quantity;

        public ShoppingCartItem(int cartId, int productId, int quantity)
        {
            Id = GetIdSequence();
            CartId = cartId;
            ProductId = productId;
            Quantity = quantity;
        }

        private static int GetIdSequence()
        {
            return _idSequence++;
        }
    }
}
