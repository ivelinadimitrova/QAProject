Feature: User

@Authenticate
Scenario: GET all users
	Given I want to prepare a request
	When I get all users from the users endpoint
	Then The response status code should be OK
		And the response should contain a list of users

@Authenticate
Scenario Outline: Create a new user
	Given I have the following user data <name>, <email>, <gender>, <status>
	When I send a request to the users endpoint
	Then The response status code should be <statusCode>
		And The user should be created successfully

Examples: 
| statusCode          | name      | email         | gender | status |
| Created             | Randodsds | randds@my.com | male   | active |
| UnprocessableEntity |           | duc@my.com    | male   | active |


Scenario: Update an existing user
	Given I have a created user already
	When I send update request to the users endpoint
	Then The response status code should be OK
		And The user should be updated successfully