# Selenium.Tests.SDK
SDK for creation selenium tests.

Includes the SpecFlow framework configured with example.


Dependencies.
- Specflow needs to be installed: https://docs.specflow.org/projects/specflow/en/latest/Installation/Installation.html
- The rest packages might be restored via nuget manager (check packages.config).

Remote run.
- In order to use remote run, set selenoid hub end point in the seleniumSettings.json
("Endpoint": "SELENOID HUB ENDPOINT").
- Change BrowserSessionSettings -> "Mode": "remote" .

Selenoid should be configured and be able to support the specified browser capabilities.
