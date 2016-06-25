Feature: Tests
	In order to avoid silly mistakes
	We do silly tests
	So we can try not to fuck shit up

@mytag
Scenario: Shopping Cart Checkout
	Given I have this ShoppingCart item 
		| Id | User | State |  Date  |
		| 1  |Carlos|Pending|24/06/16|
	And The items in the cart are 
		| Id | CartId | ProductId | Quantity |
		| 0  |   1    |    1      |     2    | 
		| 1  |   1    |    2      |     3    |
		| 2  |   1    |    3      |     1    |
	And There is enough inventory
	When I make the checkout
	Then The total amount of the cart is 1800
	And We subtract the quantity buyed
		| Id | ProductId | Quantity | Type |   Date   |
		| 1  |    1      |    3     | Sale | 10/06/16 |
		| 2  |    2      |    2     | Sale | 10/06/16 |
		| 3  |    3      |    4     | Sale | 10/06/16 |