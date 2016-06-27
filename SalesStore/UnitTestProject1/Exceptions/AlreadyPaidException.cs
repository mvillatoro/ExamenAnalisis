using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.Exceptions
{
    public class AlreadyPaidException :Exception
    {
        public AlreadyPaidException(string message):base (message)
        {
            
        }
    }
}
