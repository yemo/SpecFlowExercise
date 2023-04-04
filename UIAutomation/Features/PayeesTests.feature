Feature: Payees
	Payees test cases in UI Test Automation excercise

Background: 
	Given launch BNZ demo website

@smoke
Scenario: Verify user can navigate to Payees page using the navigation menu
	When I click the Menu button
	And I select the Payees
	Then I verify Payees page is loaded


Scenario: Verify you can add new payee in the Payees page
	Given I navigate to Payees page
	When I click Add new payee button
	And I enter the new payee details
	And I click Add button on the new payee details page
	Then I verify Payee added message is displayed
	And I verify payee is added in the list of payees

Scenario: Verify payee name is a required field
	Given I navigate to Payees page
	When I click Add new payee button
	And I click Add button on the new payee details page
	Then I verify the Validate errors is displayed
	When I enter payee name on the new payee details page
	Then I verify the Validate errors are gone

Scenario: Verify that payees can be sorted by name
	Given I navigate to Payees page
	When I add a new payee
	Then I verify list is sorted in ascending order by default
	When I click Name header
	Then I verify list is sorted in descending order

