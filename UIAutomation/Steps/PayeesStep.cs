using UIAutomation.PageObjects;
using UIAutomation.Utilities;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
using FluentAssertions;

namespace UIAutomation.Steps
{
    [Binding]
    public class PayeesStep
    {
        private readonly PayeesPage _page;
        private readonly ISpecFlowOutputHelper _log;

        public PayeesStep(IWebDriver driver, ISpecFlowOutputHelper outputHelper)
        {
            _log = outputHelper;
            _page = new PayeesPage(driver, _log);
        }

        [When(@"I click Add new payee button")]
        public void IClickAddNewPayeeButton()
        {
            _page.OpenAddNewPayeesForm();
        }

        [When(@"I enter the new payee details")]
        [System.Obsolete]
        public void IEnterTheNewPayeeRandomDetails()
        {
            string name = General.RandomString(8);
            ScenarioContext.Current.Add("PayeeName", name);
            _log.WriteLine("Enter name: " + name);
            _page.EnterPayeeName(name);
            _page.EnterPayeeAccount("02", General.RandomNumber(4), General.RandomNumber(7), "000");
        }

        [When(@"I click Add button on the new payee details page")]
        public void IClickAddButtonOnTheNewPayeeDetailsPage()
        {
            _page.AddNewPayee();
        }

        [When(@"I enter payee name on the new payee details page")]
        public void IEnterPayeeNameOnTheNewPayeeDetailsPage()
        {
            string name = General.RandomString(8);
            _page.EnterPayeeName(name);
        }

        [When(@"I add a new payee")]
        [System.Obsolete]
        public void IAddANewPayee()
        {
            IClickAddNewPayeeButton();
            IEnterTheNewPayeeRandomDetails();
            IClickAddButtonOnTheNewPayeeDetailsPage();
        }

        [When(@"I click Name header")]
        public void IClickNameHeader()
        {
            _page.ResordedByName();
        }
        
        [Then(@"I verify Payees page is loaded")]
        public void IVerifyPayeesPageIsLoaded()
        {
            _page.VerifyPayeesPage();
        }

        [Then(@"I verify Payee added message is displayed")]
        public void IverifyPayeeAddedMessageIsDisplayed()
        {
            string msg = _page.GetMessage();
            msg.Should().Be("Payee added");
        }

        [Then(@"I verify payee is added in the list of payees")]
        [System.Obsolete]
        public void IVerfityPayeeHasAdded()
        {
            _page.VerifyPayeeOnTheList((string)ScenarioContext.Current["PayeeName"]);
        }

        [Then(@"I verify the Validate errors is displayed")]
        public void IVerfiyTheValidatErrorsIsDisplayed()
        {
            _page.VerifyErrorDisplayed(1);
        }
        
        [Then(@"I verify the Validate errors are gone")]
        public void IVerfiyTheValidateErrorsAreGone()
        {
            _page.VerifyErrorDisplayed(0);
        }

        [Then(@"I verify list is sorted in ascending order by default")]
        public void IVerfiyListIsSortedInAscendingOrderByDefault()
        {
            _page.VerifyAscending();
        }

        [Then(@"I verify list is sorted in descending order")]
        public void IVerfiyListIsSortedInDescendingOrder()
        {
            _page.VerfiyDescending();
        }

    }
}
