using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestProject1.Exceptions;
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
        private readonly IMailManager _mailManager;
      

        public Store(IProductRepository productRepository, IInventoryOperationRepository inventoryOperationRepository, IShoppingCartItem shoppingCartItem, IMailManager mailManager, IShoppingCartRepository shoppingCartRepository)
        {

            _productRepository = productRepository;
            _inventoryOperationRepository = inventoryOperationRepository;
            _shoppingCartItem = shoppingCartItem;
            _mailManager = mailManager;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public Store(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public void SendProductNotification(string message)
        {
            _mailManager.SendEmail(message);
        }

        private bool CheckProductExistance(int productId, int quantity)
        {
            var existance = false;
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
            existance = (boughtQuantity - soldQuantity)>=quantity;
            if (!existance)
            {
                SendProductNotification("We need to buy more " + _productRepository.GetProduct(productId));
            }
            return existance;
        }

        public bool CheckExistance(int cartId)
        {
            var items = _shoppingCartItem.GetByCart(cartId);
            if(items!=null)
            foreach (var item in items)
            {
                var valid = CheckProductExistance(item.ProductId, item.Quantity);
                if (!valid)
                    return false;
            }
            return true;
        }

        private bool SellItem(int productId,int quantity)
        {
            var inventory = _inventoryOperationRepository.GetInventoryOperation(productId);
            if (inventory.ReduceQuantity(quantity))
            {
                _inventoryOperationRepository.Insert(new InventoryOperationInsertModel
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Type = "Sale",
                    Date = DateTime.Today
                });
                return true;
            }
            return false;
        }

        public float CheckOut(int cartId)
        {
            float price = 0.0f;
            var items = _shoppingCartItem.GetByCart(cartId);
            var cart = _shoppingCartRepository.Get(cartId);
            if(cart.CartState.Equals("Paid"))
                throw new AlreadyPaidException("Cart Already Paid");
            foreach (var item in items)
            {
                if(SellItem(item.ProductId, item.Quantity))
                    price += _productRepository.GetProduct(item.ProductId).ProductPrice*item.Quantity;  
            }
            return price;
        }

        public List<ShoppingCart> GetUserPendingCarts(string username)
        {
            var carts = _shoppingCartRepository.GetPending();
            List<ShoppingCart> userCarts = new List<ShoppingCart>();
            foreach(var shoppingCart in carts)
            {
                if(shoppingCart.CartUserName == username)
                {
                    userCarts.Add(shoppingCart);
                }
            }
            return userCarts;
        }
    }
}
