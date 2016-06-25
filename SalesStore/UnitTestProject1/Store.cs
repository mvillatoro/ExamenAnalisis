using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestProject1.Interfaces;
using UnitTestProject1.Models;

namespace UnitTestProject1
{
    public class Store
    {
        private readonly IInventoryOperationRepository _inventoryOperationRepository;
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCartItem _shoppingCartItem;
        private readonly IShoppingCartRepository _shoppingCartRepository;
      

        public Store(IProductRepository productRepository, IInventoryOperationRepository inventoryOperationRepository, IShoppingCartItem shoppingCartItem)
        {

            _productRepository = productRepository;
            _inventoryOperationRepository = inventoryOperationRepository;
            _shoppingCartItem = shoppingCartItem;
        }

        private bool CheckProductExistance(int productId)
        {
            var products = _productRepository.GetAll();
            var inventoryOperations = _inventoryOperationRepository.GetAll();
            int boughtQuantity=0;
            int soldQuantity = 0;
            foreach (var prod in products)
            {
                if (prod.ProductId == productId)
                {
                    boughtQuantity = inventoryOperations.Where(x => x.ProductId == productId && x.Type == "Purchase")
                        .Select(x => x.ProductQuatity).ToList().Sum();
                    soldQuantity = inventoryOperations.Where(x => x.ProductId == productId && x.Type == "Sale")
                       .Select(x => x.ProductQuatity).ToList().Sum();
                }
            }
            return (boughtQuantity - soldQuantity)>0;
        }

        public bool CheckExistance(int cartId)
        {
            var items = _shoppingCartItem.GetByCart(cartId);
            foreach (var item in items)
            {
                var valid = CheckProductExistance(item.ProductId);
                if (!valid)
                    return false;
            }
            return true;
        }

        private void SellItem(int productId,int quantity)
        {
            var inventory = _inventoryOperationRepository.GetInventoryOperation(productId);
            inventory.ReduceQuantity(quantity);
            _inventoryOperationRepository.Insert( new InventoryOperationInsertModel
            {
                ProductId = productId,
                Quantity = quantity,
                Type = "Sale",
                Date = DateTime.Today 
            });
        }
        public float CheckOut(int cartId)
        {
            float price = 0.0f;
            var items = _shoppingCartItem.GetByCart(cartId);
            foreach (var item in items)
            {
                price += _productRepository.GetProduct(item.ProductId).ProductPrice*item.Quantity;
                SellItem(item.ProductId, item.Quantity);
            }
            return price;
        }
    }
}
