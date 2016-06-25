using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    public class ShoppingCart
    {
        private static int _idSequence = 1;
        public int CartId;
        public string CartUserName;
        public string CartState;
        public DateTime CartCreationTime;  

        public ShoppingCart(int cartId,string cartUserName, string cartState, DateTime cartCreationTime)
        {
            CartId = cartId;
            CartUserName = cartUserName;
            CartState = cartState;
            CartCreationTime = cartCreationTime;
        }

        public static int GetNextId()
        {
            return _idSequence++;
        }

        public static void ResetSequence()
        {
            _idSequence = 1;
        }
    }
}
