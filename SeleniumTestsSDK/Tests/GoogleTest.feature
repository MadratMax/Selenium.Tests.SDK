Feature: GoogleTest
	As a User I want to be able to use Google search

@smoke
Scenario: Wiki link for nginx
	Given user opens page 'GoogleHomePage'
	When user clicks on 'SearchBox'
	And user enters the text 'nginx'
	And element 'SearchBox' text is 'nginx'
	And user clicks on 'SearchButton'
	And element 'WikipediaLink' is displayed