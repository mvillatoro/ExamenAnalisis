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
        private static int _idSequence = 1;
        public int OperationId;
        public int ProductId;
        public int ProductQuatity;
        public string Type;
        public DateTime CheckInTime;

        public InventoryOperation(int operationId, int productId, int productQuantity, string type, DateTime checkInTime)
        {
            OperationId = operationId;
            ProductId = productId;
            ProductQuatity = productQuantity;
            Type = type;
            CheckInTime = checkInTime;
        }

        public static int GetIdSequence()
        {
            return _idSequence++;
        }

        public void ReduceQuantity(int quantity)
        {
            if(this.ProductQuatity>quantity)
                this.ProductQuatity -= quantity;
        }
    }
}
