namespace UnitTestProject1.Entites
{
    public class ShoppingCartItem
    {
        private static int _idSequence = 1;

        private int _id;
        public int CartId;
        public int ProductId;
        public int Quantity;

        public ShoppingCartItem(int cartItemId, int cartId, int productId, int quantity)
        {
            _id = cartItemId;
            CartId = cartId;
            ProductId = productId;
            Quantity = quantity;
        }

        public static int GetIdSequence()
        {
            return _idSequence++;
        }

        public static void ResetSequence()
        {
            _idSequence = 1;
        }
    }
}
