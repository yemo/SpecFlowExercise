using UIAutomation.PageObjects;
using UIAutomation.Utilities;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
using System.Globalization;
using FluentAssertions;

namespace UIAutomation.Steps
{
    [Binding]
    public class PaymentsStep
    {
        private readonly PaymentsPage _page;
        private readonly ISpecFlowOutputHelper _log;

        public PaymentsStep(IWebDriver driver, ISpecFlowOutputHelper outputHelper)
        {
            _log = outputHelper;
            _page = new PaymentsPage(driver, _log);
        }

        [When(@"I transfer \$(.*) from (.*) account to (.*) account")]
        [System.Obsolete]
        public void ITransferFromAccountToTheOtherAccount(string amount, string from, string to)
        {
            string BalanceFromAcc = _page.SelectFromAccountAndGetBalance(from);
            string BalanceToAcc = _page.SelectToAccountAndGetBalance(to);
            _page.EnterAmount(amount);
            ScenarioContext.Current.Add("BalanceFromAcc", BalanceFromAcc);
            ScenarioContext.Current.Add("BalanceToAcc", BalanceToAcc);
            ScenarioContext.Current.Add("TransferAmount", amount);
            _log.WriteLine("Transfer $" + amount + " from " + from + "($" + BalanceFromAcc + ") to " + to + "($" + BalanceToAcc + ").");
            _page.MakeTransfer();
        }

        [Then(@"I verify transfer successful message is displayed")]
        public void IVerifyTransferSuccessfulMessageIsDisplayed()
        {
            string msg = _page.GetMessage();
            msg.Should().Be("Transfer successful");
        }

        [Then(@"I verify the current balance of (.*) account and (.*) account are correct")]
        [System.Obsolete]
        public void IVerifyAccountBalanceAfterTransfer(string from, string to)
        {
            string CurrentBalanceFromAcc = _page.GetAccountBalance(from);
            string CurrentBalanceToAcc = _page.GetAccountBalance(to);
            _log.WriteLine("Current Balance of From Account: $" + CurrentBalanceFromAcc);
            _log.WriteLine("Current Balance of To Account: $" + CurrentBalanceToAcc);

            //convert string to decimal for calculation
            decimal amount = decimal.Parse((string)ScenarioContext.Current["TransferAmount"], NumberStyles.Number);
            decimal FromDiff = General.BalanceDiff(CurrentBalanceFromAcc, (string)ScenarioContext.Current["BalanceFromAcc"]);
            decimal ToDiff = General.BalanceDiff(CurrentBalanceToAcc, (string)ScenarioContext.Current["BalanceToAcc"]);

            //assertion
            FromDiff.Should().Be(0-amount);
            ToDiff.Should().Be(amount);
        }
    }
}
