using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.Exceptions
{
    public class CartAlreadyPaidException : Exception
    {

        public CartAlreadyPaidException() : base("Cart Already Paid")
        {

        }
        public CartAlreadyPaidException(string err) : base(err)
        {

        }
    }
}
