Feature: APIDemo
	3 API test cases

@smoke
Scenario: Verify GET Users request
	When I send a GET Users request
	Then Verify 200 OK message is returned
	And Verify that there are 10 users in the results

Scenario Outline: Verify GET User request by Id
	When I send a GET Users request by Id <id>
	Then Verify 200 OK message is returned
	And Verify if user with id <id> is <name>
Examples:
| id | name                     |
| 8  | Nicholas Runolfsdottir V |
| 4  | Patricia Lebsack         |
| 5  | Chelsey Dietrich         |

Scenario Outline: Verify POST Users request
	When I send a POST Users request by <userid>
	Then Verify 201 Created message is returned
	And Verify that the posted data by <userid> are showing up in the result
Examples:
| userid |
| 2		 |
| 5		 |
