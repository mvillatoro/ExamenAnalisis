namespace UnitTestProject1.Entites
{
    public class ShoppingCartItem
    {
        private static int _idSequence = 1;

        private int _id;
        public int CartId;
        public int ProductId;
        public int Quantity;

        public ShoppingCartItem(int cartId, int productId, int quantity)
        {
            _id = GetIdSequence();
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
