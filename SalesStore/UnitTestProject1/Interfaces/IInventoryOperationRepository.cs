using System.Collections.Generic;
using UnitTestProject1.Models;

namespace UnitTestProject1.Interfaces
{
    public interface IInventoryOperationRepository
    {
        List<InventoryOperation> GetAll();
        InventoryOperation GetInventoryOperation(int productId);
        InventoryOperation Insert(InventoryOperationInsertModel operation);

    }
}