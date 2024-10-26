# Selenium.Tests.SDK
SDK for creating Selenium tests using Gherkin notation.
This framework serves as a foundational platform that is easy to extend, enabling the creation of automated tests without the need for redundant initial logic coding.

Includes the SpecFlow framework configured with example.


Requirements:
- Specflow needs to be installed: https://docs.specflow.org/projects/specflow/en/latest/Installation/Installation.html
- The rest packages might be restored via nuget manager (check packages.config).
- Chromedriver located in the project dir is compatible to chrome v.76. To interact with your current browser version download a desired chromedriver from here: https://chromedriver.chromium.org/downloads
- Chromedriver.exe is executable binary, hence it should be always copied into debug folder.

Dependencies:
- NUnit 3.10
- NUnit Adapter 3.13
- SpecFlow 2.3
- Polly 7.1.0

Using:
Find the SpecFlow Example test (/Tests/ExampleTest.feature).
Look at the comments under each step of the test.
To find more implemented bindings, write down a new step with 'And' key word and check the expanded list of existing step bindings.


Remote run:
- In order to use remote run, set selenoid hub end point in the seleniumSettings.json
("Endpoint": "SELENOID HUB ENDPOINT").
- Change BrowserSessionSettings -> "Mode": "remote" .

Selenoid should be configured and be able to support the specified browser capabilities.
Firefox, Opera and IE are also able to be used during the remote run (should be configured properly on the remote server). 
Refer to seleniumSettings.json to find out the preconfigured settings for all supported browsers.
