using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    public class InventoryOperation
    {
        private static int _idSequence = 0;
        public int OperationId;
        public int ProductId;
        public int ProductQuatity;
        public string Type;
        public DateTime CheckInTime;

        public InventoryOperation(int productId, int productQuantity, string type, DateTime checkInTime)
        {
            OperationId = GetIdSequence();
            ProductId = productId;
            ProductQuatity = productQuantity;
            Type = type;
            CheckInTime = checkInTime;
        }

        private static int GetIdSequence()
        {
            return _idSequence++;
        }
    }
}
