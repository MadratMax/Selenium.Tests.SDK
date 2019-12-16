Feature: SpecFlowFeature1
	As a User I want to login to 

@smoke
Scenario: Login to
	Given user opens page 'GoogleHomePage'
	When user clicks on 'SearchBox'
	And user enters the text 'nginx'
	Then element '{elementName}' text is 'nginx'