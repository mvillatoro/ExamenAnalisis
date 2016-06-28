using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.DictionaryAdapter;
using Castle.Core.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UnitTestProject1.Entites;
using UnitTestProject1.Interfaces;
using UnitTestProject1.Models;
using System.Globalization;

namespace UnitTestProject1
{
    [Binding]
    public class TestsSteps
    {
        ShoppingCart _currentShoppingCart;
        List<ShoppingCartItem> _currentShoppingCartItems = new List<ShoppingCartItem>();
        List<InventoryOperation> _inventoryOperations = new List<InventoryOperation>();
        List<Product> _products = new List<Product>();
        private Dictionary<int, int> productDetail;
        readonly Mock<IProductRepository> _productMock = new Mock<IProductRepository>();
        readonly Mock<IInventoryOperationRepository> _inventoryOpMock = new Mock<IInventoryOperationRepository>();
        readonly Mock<IMailManager> _mailManagetMock = new Mock<IMailManager>();
        Mock<IShoppingCartItem> _shoppingCartItemMock = new Mock<IShoppingCartItem>();
        private bool _existance = false;
        private float _total;
        private DateTime _referenceDate;
        List<ShoppingCart> _shoppingCartCollection = new List<ShoppingCart>();
        Mock<IShoppingCartRepository> _shoppingCartMock = new Mock<IShoppingCartRepository>();
        string _user;

        [Given(@"I have this ShoppingCart item")]
        public void GivenIHaveThisShoppingCartItem(Table table)
        {
            if(!_currentShoppingCartItems.IsNullOrEmpty())
                _currentShoppingCartItems.Clear();

            if(!_inventoryOperations.IsNullOrEmpty())
                _inventoryOperations.Clear();

            if(!_products.IsNullOrEmpty())
                _products.Clear();
            
            ShoppingCart.ResetSequence();
            Product.ResetSequence();
            ShoppingCartItem.ResetSequence();
            InventoryOperation.ResetSequence();
            

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
            var store = new Store(_productMock.Object, _inventoryOpMock.Object, _shoppingCartItemMock.Object, _mailManagetMock.Object);
            _existance = store.CheckExistance(_currentShoppingCart.CartId);
            
        }

        [Given(@"There is not  enough inventory")]
        public void GivenThereIsNotEnoughInventory()
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
            _productMock.Setup(rep => rep.GetProduct(It.IsAny<int>())).Returns((int id) => _products.Single(x => x.ProductId == id));
            _inventoryOpMock.Setup(rep => rep.GetInventoryOperation(It.IsAny<int>()))
                .Returns((int id) => _inventoryOperations.Single(x => x.OperationId == id));
            _inventoryOpMock.Setup(rep => rep.GetAll()).Returns(_inventoryOperations);
            _shoppingCartItemMock.Setup(rep => rep.GetByCart(It.IsAny<int>())).Returns(_currentShoppingCartItems);

            _inventoryOpMock.Setup(rep => rep.Insert(It.IsAny<InventoryOperationInsertModel>())).Returns(
                (InventoryOperationInsertModel operation) =>
                {
                    var Operation = new InventoryOperation(InventoryOperation.GetIdSequence(), operation.ProductId, operation.Quantity, operation.Type, operation.Date);
                    _inventoryOperations.Add(Operation);
                    return Operation;
                });
            var store = new Store(_productMock.Object, _inventoryOpMock.Object, _shoppingCartItemMock.Object, _mailManagetMock.Object);
            _existance = store.CheckExistance(_currentShoppingCart.CartId);
        }

        [When(@"I make the checkout")]
        public void WhenIMakeTheCheckout()
        {
            _total = 0.0f;
            var store = new Store(_productMock.Object, _inventoryOpMock.Object, _shoppingCartItemMock.Object, _mailManagetMock.Object);
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

        [Then(@"Send an error email")]
        public void ThenSendAnErrorEmail()
        {

                _mailManagetMock.Verify(rep=>rep.SendEmail(It.IsAny<string>()));
            
        }

        [Given(@"I have a date")]
        public void GivenIHaveADate(Table table)
        {
            _referenceDate = new DateTime(int.Parse(table.Rows[0].Values.ToList()[2]), int.Parse(table.Rows[0].Values.ToList()[0]), int.Parse(table.Rows[0].Values.ToList()[1]));
        }

        [Given(@"a list of ShoppingCarts")]
        public void GivenAListOfShoppingCarts(Table table)
        {
            _shoppingCartCollection.Clear();
            foreach(var row in table.Rows)
            {
                var date = DateTime.ParseExact(row.Values.ToList()[3], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                _shoppingCartCollection.Add(new ShoppingCart(int.Parse(row.Values.ToList()[0]), row.Values.ToList()[1], row.Values.ToList()[2],date));
            }
        }

        [Given(@"the user ""(.*)""")]
        public void GivenTheUser(string p0)
        {
            _user = p0;
        }


        [Then(@"we get the ShoppingCart")]
        public void ThenWeGetTheShoppingCart(Table table)
        {
            _shoppingCartMock.Setup(rep => rep.GetPending()).Returns(_shoppingCartCollection.Where(cart => cart.CartState == "Pending" && (cart.CartCreationTime.Month- _referenceDate.Month) + 12 * (cart.CartCreationTime.Year- _referenceDate.Year) >= 1).ToList());
            Store store = new Store(_shoppingCartMock.Object);
            var pendingCarts = store.GetUserPendingCarts(_user);
            Assert.AreEqual(pendingCarts[0].CartId, int.Parse(table.Rows[0].Values.ToList()[0]));
        }

        [Given(@"I have this products")]
        public void GivenIHaveThisProducts(Table table)
        {
            if (!_currentShoppingCartItems.IsNullOrEmpty())
                _currentShoppingCartItems.Clear();

            if (!_inventoryOperations.IsNullOrEmpty())
                _inventoryOperations.Clear();

            if (!_products.IsNullOrEmpty())
                _products.Clear();

            ShoppingCart.ResetSequence();
            Product.ResetSequence();
            ShoppingCartItem.ResetSequence();
            InventoryOperation.ResetSequence();


            for (int i = 0; i < table.RowCount; i++)
            {
                var productCode = table.Rows[i].Values.ToList().ElementAt(1);
                var productName = table.Rows[i].Values.ToList().ElementAt(2);
                var productDescription = table.Rows[i].Values.ToList().ElementAt(3);
                var productprice = float.Parse(table.Rows[i].Values.ToList().ElementAt(4));
                var p = Product.GetNextId();
                _products.Add(new Product(p, productCode, productName, productDescription, productprice));
            }
        }

        [Given(@"I have an list of inventory movements")]
        public void GivenIHaveAnListOfInventoryMovements(Table table)
        {
            for (int i = 0; i < table.RowCount; i++)
            {
                var productId = Int32.Parse(table.Rows[i].Values.ToList().ElementAt(1));
                var productQuantity = Int32.Parse(table.Rows[i].Values.ToList().ElementAt(2));
                var type = table.Rows[i].Values.ToList().ElementAt(3);
                var date = Convert.ToDateTime(table.Rows[i].Values.ToList().ElementAt(4));
                _inventoryOperations.Add(new InventoryOperation(InventoryOperation.GetIdSequence(), productId, productQuantity, type, date));
            }
        }

        [When(@"I create the report")]
        public void WhenICreateTheReport()
        {
            _productMock.Setup(rep => rep.GetAll()).Returns(_products);
            _inventoryOpMock.Setup(rep => rep.GetAll()).Returns(_inventoryOperations);
            Store store = new Store(_productMock.Object, _inventoryOpMock.Object, _shoppingCartItemMock.Object, _mailManagetMock.Object);

            productDetail = store.PrintStockReport(_inventoryOperations, _products);
        }

        [Then(@"The total products in stock must be correct")]
        public void ThenTheTotalProductsInStockMustBeCorrect(Table table)
        {
            Dictionary<int, int> detailTable = new Dictionary<int, int>();
            for(int i = 0; i< table.RowCount; i++)
            {
                var value = table.Rows[i].Values.ToList();
                detailTable.Add(Int32.Parse(value.ElementAt(0)), Int32.Parse(value.ElementAt(1)));
            }
            CollectionAssert.AreEqual(productDetail, detailTable);
        }
    }
}