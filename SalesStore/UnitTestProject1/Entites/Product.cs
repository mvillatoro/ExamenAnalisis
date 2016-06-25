using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    public class Product
    {
        private static int _idSequence = 0;
        public int ProductId;
        public string ProductCode;
        public string ProductName;
        public string ProductDescription;
        public float ProductPrice;

        public Product(string productCode, string productName, string productDescription, float productPrice)
        {
            ProductId = GetNextId();
            ProductCode = productCode;
            ProductName = productName;
            ProductDescription = productDescription;
            ProductPrice = productPrice;
        }

        private static int GetNextId()
        {
            return _idSequence++;
        }
    }
}
