Feature: UserDetailsFeature

As a user , i should be allow to create new user , update existing user , get list of users and delete the user.

@Post @Positive
Scenario Outline: Add a new user
Given I have new user information with name as <UserName> and Job as <Job>
When I create new user
Then I should receive the status code as 201
And I should see username as <UserName> in create user response
And I should see Job as <Job> in create user response

Examples: 
| UserName | Job     |
| Maria    | Analyst |
| John     | AM      |

@Get @Positive
Scenario Outline: Get user details
    Given an user with ID <UserID>
    When I get user details
    Then I should receive the status code as 200
    And I should see Email as <EMail> in get user response
    And I should see Avatar as <Avatar> in get user response

    
Examples: 
| UserID | EMail                      | Avatar                                  |
| 7      | michael.lawson@reqres.in   | https://reqres.in/img/faces/7-image.jpg |
| 8      | lindsay.ferguson@reqres.in | https://reqres.in/img/faces/8-image.jpg |

@Put @Positive
Scenario Outline: Update user details
 Given an user with ID <UserID>
  And I have new user information with name as <UserName> and Job as <Job>
  When I Update user details
  Then I should receive the status code as 200
  And I should see Job as <Job> in Update user response

Examples: 
| UserID | UserName | Job |
| 2      | morpheus    | zion resident    |

@Delete @Positive
Scenario: Delete the valid user
Given an user with ID 4
When I Delete user details
Then I should receive the status code as 204

@Post @Negative
Scenario Outline: Attempt to Add a New User with Incomplete Information
    Given I have incomplete user information with name as <UserName> and Job as <Job>
    When I attempt to create a new user with incomplete information
    Then I should receive a validation error
  
Examples:
  | UserName | Job     |
  | ""       | Analyst |
  | John     |         |
  |          |         |

 @Delete @Negative
 Scenario Outline: Attempt to Delete User with Invalid ID
    Given a user with ID <UserID> does not exist
    When I attempt to delete the user with Invalid ID
    Then the system should respond with a user not found error
Examples:
  | UserID     |
  | invalid123 |
  | 999        |
  |            |

