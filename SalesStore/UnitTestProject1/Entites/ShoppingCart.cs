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

        public ShoppingCart(string cartUserName, string cartState, DateTime cartCreationTime)
        {
            CartId = GetNextId();
            CartUserName = cartUserName;
            CartState = cartState;
            CartCreationTime = cartCreationTime;
        }

        private static int GetNextId()
        {
            return _idSequence++;
        }
    }
}
