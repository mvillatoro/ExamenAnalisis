using System;
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
            ScenarioContext.Current.Pending();
        }
    }
}
