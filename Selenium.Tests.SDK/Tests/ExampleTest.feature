Feature: ExampleTest
	As a User I want to be able to use Google search

@smoke
Scenario: Search found a link for nginx
	# open page by url. page is defined by identical page name class (GoogleHomePage.cs). this page is current page in test context
	Given user opens page 'GoogleHomePage'
	# click by element with the same name as in the desired page class (IWebElement SearchBox)
	When user clicks on 'SearchBox'
	# enter a text into the element that was found before (it's current element in the test context)
	And user enters the text 'nginx'
	# verify if the element text is equal to the desired text
	And element 'SearchBox' text is 'nginx'
	# click by element with the same name as in the desired page class (IWebElement SearchButton)
	And user clicks on 'SearchButton'
	# check if the element is displayed in current page (the current page is still GoogleHomePage)
	And element 'NginxLink' is displayed