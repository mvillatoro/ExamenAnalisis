Feature: SalesStore
	In order to avoid silly mistakes

Scenario: Read movement file 
	Given I have a file named 'moves.txt'
	When I read the file
	Then contents will be 'Player1;Player2|Player1:L,Player2:R'





