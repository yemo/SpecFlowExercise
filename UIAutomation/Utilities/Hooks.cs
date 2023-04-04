using BoDi;
using OpenQA.Selenium;
using System.IO;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;


namespace UIAutomation.Utilities
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _objectContainer;
        private IWebDriver _driver;
        private static DriverFactory _driverFactory;
        private readonly ISpecFlowOutputHelper _log;

        public Hooks(IObjectContainer objectContainer, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            _objectContainer = objectContainer;
            _log = specFlowOutputHelper;
        }

        //initial driver
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _driverFactory = new DriverFactory();
            Directory.CreateDirectory(Path.Combine("..", "..", "TestResults"));
        }

        [BeforeScenario(Order = 0)]
        public void BeforeScenario()
        {
            _driver = _driverFactory.CreateDriver();
            _objectContainer.RegisterInstanceAs(_driver);
        }

        //Take Screenshot for every steps and attach to SpecFlow+ LivingDoc
        [AfterStep()]
        public void TakeScreenshotAfterEachStep()
        {
            var filename = Path.Combine("..", "..", "TestResults", Path.ChangeExtension(Path.GetRandomFileName(), "png"));
            ((ITakesScreenshot)_driver).GetScreenshot().SaveAsFile(filename, ScreenshotImageFormat.Png);
            _log.AddAttachment(filename);
        }

        //close driver and take screenshot if test failed
        [AfterScenario]
        public void AfterScenario(ScenarioContext scenarioContext)
        {
            if (scenarioContext.TestError != null)
            {
                ((ITakesScreenshot)_driver).GetScreenshot().SaveAsFile(Path.Combine("..", "..", "TestResults", $"{scenarioContext.ScenarioInfo.Title}.png"), ScreenshotImageFormat.Png);
            }
            _driver?.Dispose();
        }
    }
}
