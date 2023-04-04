Feature: Payments
	Payments test cases in UI Test Automation excercise

Background: 
	Given launch BNZ demo website

@smoke
Scenario: Navigate to Payments page
	Given I navigate to Payments page
	When I transfer $500 from Everyday account to Bills account
	Then I verify transfer successful message is displayed
	And I verify the current balance of Everyday account and Bills account are correct