using UIAutomation.PageObjects;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace UIAutomation.Steps
{
    [Binding]
    public class ClientStep
    {
        private readonly ClientPage _page;
        private readonly ISpecFlowOutputHelper _log;

        public ClientStep(IWebDriver driver, ISpecFlowOutputHelper outputHelper)
        {
            _page = new ClientPage(driver);
            _log = outputHelper;
        }

        [Given(@"launch BNZ demo website")]
        public void LaunchBNZDemoWebsite()
        {
            _page.LaunchPage();
        }

        [Given(@"I navigate to Payees page")]
        public void INavigateToPayeesPage()
        {
            _page.ClickMenu();
            _page.ClickPayee();
        }

        [Given(@"I navigate to Payments page")]
        public void INavigateToPaymentsPage()
        {
            _page.ClickMenu();
            _page.ClickPayment();
        }

        [When(@"I click the Menu button")]
        public void IClickTheMenuButton()
        {
            _page.ClickMenu();
        }

        [When(@"I select the Payees")]
        public void ISelectThePayees()
        {
            _page.ClickPayee();
        }
    }
}
