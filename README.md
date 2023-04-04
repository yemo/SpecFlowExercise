# SpecFlow.Excecies

The goal of this project is to demo automation test sets UI and API automation test by SpecFlow

**Resources**
- [Testing pyramid](https://learn.cypress.io/testing-foundations/the-testing-pyramid)
- [SpecFlow](http://specflow.org/)
- [FluentAssertions](https://fluentassertions.com/)
- [Selenium](http://www.seleniumhq.org/)
- [SpecFlow+LivingDoc](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/)
- [Azure DevOps](https://learn.microsoft.com/en-us/azure/devops/?view=azure-devops)




### TODO
- [x] SpecFlow E2E UI automation Framework
- [x] SpecFlow API automation Framework
- [x] Azure DevOps test execution pipeline
- [x] Azure DevOps SpecFlow+LivingDoc pages
- [x] Run UI automation in different brower
- [x] Parameterise tests
- [x] Run the tests in parallel


### Environment Variaber
- BROWSER (CHROME, FIREFOX, IE)

### Azure DevOps Pipeline yaml file

- SpecFlowExercise-CI.yml

### Setup - Windows OS

- Download and install [Visual Studio](https://visualstudio.microsoft.com/)

- Download and install [Chrome Browser](https://support.google.com/chrome/answer/95346?hl=en&co=GENIE.Platform%3DDesktop#zippy=%2Cwindows)

- Download and add to Environment Path [Chrome WebDriver](https://chromedriver.chromium.org/downloads)



### Visual Studio

Visual Studio needs a little extra configuration. Install these extensions;
- NUnit
- SpecFlow
- add vstest.concole.exe to Environment Path if run through the command line





**Run Test using Visual Studio**
- Build the solution
- Right click the project and select 'Run Tests'


**Run Test using Windows Command Line**

Restore dependencies:
```
> nuget restore
```

Build:
```
> msbuild .\SpecflowExcecise.sln
```

Run UI tests by VSTest
```
> vstest.console.exe UIAutomation\bin\Debug\netcoreapp3.1\UIAutomation.dll /Logger:trx
```

Run UI tests by SpecFlow
```
> specflow.exe stepdefinitionreport --ProjectFile UIAutomation\UIAutomation.csproj /BinFolder:UIAutomation\bin\debug
```