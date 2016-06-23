using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using SalesStore;
using UnitTestProject1;

namespace SalesStore
{
    class Program
    {
        static void Main(string[] args)
        {
            var mierda1 = new Product("p12","Penes", "Grosos venosos", 12);
            var mierda2 = new Product("h54", "Dildo", "Azul y verde", 26);


            Console.WriteLine(mierda1.GetProductId());
            Console.WriteLine(mierda2.GetProductId());
            Console.ReadKey();
        }
    }
}
