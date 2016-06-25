using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    public class Product
    {
        private static int _idSequence = 1;
        public int ProductId;
        public string ProductCode;
        public string ProductName;
        public string ProductDescription;
        public float ProductPrice;

        public Product(int productId, string productCode, string productName, string productDescription, float productPrice)
        {
            ProductId = productId;
            ProductCode = productCode;
            ProductName = productName;
            ProductDescription = productDescription;
            ProductPrice = productPrice;
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
