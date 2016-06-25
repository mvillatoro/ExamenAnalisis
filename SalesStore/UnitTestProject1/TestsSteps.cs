using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.DictionaryAdapter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UnitTestProject1.Entites;
using UnitTestProject1.Interfaces;
using UnitTestProject1.Models;

namespace UnitTestProject1
{
    [Binding]
    public class TestsSteps
    {
        ShoppingCart _currentShoppingCart;
        List<ShoppingCartItem> _currentShoppingCartItems = new List<ShoppingCartItem>();
        List<InventoryOperation> _inventoryOperations;
        List<Product> _products;
        readonly Mock<IProductRepository> _productMock = new Mock<IProductRepository>();
        readonly Mock<IInventoryOperationRepository> _inventoryOpMock = new Mock<IInventoryOperationRepository>();
        Mock<IShoppingCartItem> _shoppingCartItemMock = new Mock<IShoppingCartItem>();
        private bool _existance = false;
        private float _total;


        [Given(@"I have this ShoppingCart item")]
        public void GivenIHaveThisShoppingCartItem(Table table)
        {
            for (int i = 0; i < table.RowCount; i++)
            {
                var user = table.Rows[i].Values.ToList()[1];
                var state = table.Rows[i].Values.ToList()[2];
                var date = table.Rows[i].Values.ToList()[3];
                _currentShoppingCart= new ShoppingCart(ShoppingCart.GetNextId() ,user, state, Convert.ToDateTime(date));
            }
        }
        
        [Given(@"The items in the cart are")]
        public void GivenTheItemsInTheCartAre(Table table)
        {
            for (int i=0; i<table.RowCount;i++)
            {
                var cartId = Int32.Parse(table.Rows[i].Values.ToList().ElementAt(1));
                var productId = Int32.Parse(table.Rows[i].Values.ToList().ElementAt(2));
                var quantity = Int32.Parse(table.Rows[i].Values.ToList().ElementAt(3));
                _currentShoppingCartItems.Add(new ShoppingCartItem(ShoppingCartItem.GetIdSequence() ,cartId, productId, quantity));
            }
        }
        
        [Given(@"There is enough inventory")]
        public void GivenThereIsEnoughInventory()
        {
            _products = new List<Product>()
            {
                new Product(Product.GetNextId(), "001", "USB", "2 GB Black", 200f),
                new Product(Product.GetNextId(),"002", "Montior", "40 pulgadas", 300f),
                new Product(Product.GetNextId(),"003", "SDD", "1 TB Black", 500f)
            };
            _inventoryOperations = new List<InventoryOperation>()
            {
                new InventoryOperation(InventoryOperation.GetIdSequence(), 1,5,"Purchase",DateTime.Now),
                new InventoryOperation(InventoryOperation.GetIdSequence(), 2,5,"Purchase",DateTime.Now),
                new InventoryOperation(InventoryOperation.GetIdSequence(),3,5,"Purchase",DateTime.Now)
            };
            _productMock.Setup(rep => rep.GetAll()).Returns(_products);
            _productMock.Setup(rep => rep.GetProduct(It.IsAny<int>())).Returns((int id)=> _products.Single(x => x.ProductId==id));
            _inventoryOpMock.Setup(rep => rep.GetInventoryOperation(It.IsAny<int>()))
                .Returns((int id)=>_inventoryOperations.Single(x=>x.OperationId==id));
            _inventoryOpMock.Setup(rep => rep.GetAll()).Returns(_inventoryOperations);
            _shoppingCartItemMock.Setup(rep => rep.GetByCart(It.IsAny<int>())).Returns(_currentShoppingCartItems);

            _inventoryOpMock.Setup(rep => rep.Insert(It.IsAny<InventoryOperationInsertModel>())).Returns(
                (InventoryOperationInsertModel operation) =>
                {
                     var Operation =  new InventoryOperation(InventoryOperation.GetIdSequence(), operation.ProductId, operation.Quantity, operation.Type, operation.Date);
                    _inventoryOperations.Add(Operation);
                    return Operation;
                });
            var store = new Store(_productMock.Object, _inventoryOpMock.Object, _shoppingCartItemMock.Object);
            _existance = store.CheckExistance(_currentShoppingCart.CartId);
            
        }

        [When(@"I make the checkout")]
        public void WhenIMakeTheCheckout()
        {
            _total = 0.0f;
            var store = new Store(_productMock.Object, _inventoryOpMock.Object, _shoppingCartItemMock.Object);
            if (_existance)
                _total = store.CheckOut(_currentShoppingCart.CartId);
        }

        [Then(@"The total amount of the cart is (.*)")]
        public void ThenTheTotalAmountOfTheCartIs(int p0)
        {
           Assert.AreEqual(_total,p0);
        }
        
        [Then(@"We subtract the quantity buyed")]
        public void ThenWeSubtractTheQuantityBuyed(Table table)
        {
            var cont = 0;
            foreach (var operation in _inventoryOperations)
            {
                if (operation.OperationId== int.Parse(table.Rows[cont].Values.ToList().ElementAt(0)))
                {
                    cont++;
                }
            }
            Assert.AreEqual(_inventoryOperations.Count ,cont);
        }

        [Given(@"There is not  enough inventory")]
        public void GivenThereIsNotEnoughInventory()
        {
            
        }

        [Then(@"Send an error email")]
        public void ThenSendAnErrorEmail()
        {
            ScenarioContext.Current.Pending();
        }



    }
}
