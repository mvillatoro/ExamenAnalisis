using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.Models
{
    public class InventoryOperationInsertModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; }
        public DateTime Date{ get; set; }
    }
}
