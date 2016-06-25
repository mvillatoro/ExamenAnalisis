using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace UnitTestProject1
{
    [Binding]
    public class TestsSteps
    {
        [Given(@"I have this ShoppingCart item")]
        public void GivenIHaveThisShoppingCartItem(Table table)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"The items in the cart are")]
        public void GivenTheItemsInTheCartAre(Table table)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"There is enough inventory")]
        public void GivenThereIsEnoughInventory()
        {
            var products = new List<Product>()
            {
                new Product("001", "USB", "2 GB Black", 99.98f),
                new Product("002", "Monitor", "40 pulgadas", 350.95f),
                new Product("003", "SDD", "1 TB Black", 498f)
            };

            ScenarioContext.Current.Pending();
        }
        
        [When(@"I make the checkout")]
        public void WhenIMakeTheCheckout()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"The total amount of the cart is (.*)")]
        public void ThenTheTotalAmountOfTheCartIs(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"We subtract the quantity buyed")]
        public void ThenWeSubtractTheQuantityBuyed(Table table)
        {
            var inventoryOperations = new List<InventoryOperation>()
            {
                new InventoryOperation(1,5,"Purchase",DateTime.Now),
                new InventoryOperation(2,5,"Purchase",DateTime.Now),
                new InventoryOperation(3,5,"Purchase",DateTime.Now)
            };

            ScenarioContext.Current.Pending();
        }
    }
}
