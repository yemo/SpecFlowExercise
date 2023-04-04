using OpenQA.Selenium;
using UIAutomation.Utilities;
using FluentAssertions;
using System.Collections.Generic;
using TechTalk.SpecFlow.Infrastructure;

namespace UIAutomation.PageObjects
{
    public class PayeesPage : ClientPage
    {
        private readonly IWebDriver _driver;
        private readonly ISpecFlowOutputHelper _log;
        public PayeesPage(IWebDriver driver, ISpecFlowOutputHelper log) : base(driver)
        {
            _driver = driver;
            _log = log;
        }

        public void VerifyPayeesPage()
        {
            lblTitle.Text.Should().Be("Payees");
        }

        public void OpenAddNewPayeesForm()
        {
            btnAdd.Click();
            Extensions.WaitForVisible(_driver, By.XPath("//input[@id='ComboboxInput-apm-name']"));
        }

        public void AddNewPayee()
        {
            btnAddPayee.Click();
        }

        public void EnterPayeeName(string name)
        {
            txtPayeeName.SendKeys(name);
            ddlNewPayee.Click();
        }

        public void EnterPayeeAccount(string bank,string branch, string account, string suffix)
        {
            txtBank.SendKeys(bank);
            txtBranch.SendKeys(branch);
            txtAccount.SendKeys(account);
            txtSuffix.SendKeys(suffix);
        }

        public void VerifyPayeeOnTheList(string name)
        {
            IList<IWebElement> PayeeFound = _driver.FindElements(By.XPath("//p[@class='Avatar-title']/span[contains(.,'" + name + "')]"));
            PayeeFound.Count.Should().Be(1);
        }

        public void VerifyErrorDisplayed(int count)
        {
            IList<IWebElement> ErrorFound = _driver.FindElements(By.XPath("//div[@class='error-header']"));
            ErrorFound.Count.Should().Be(count);
        }

        public void VerifyAscending()
        {
            IList<IWebElement> Payees = getPayees();
            string previous = "";
            foreach (var e in Payees)
            {
                if (previous == "")
                {
                    previous = e.Text;
                } 
                else
                {
                    _log.WriteLine(previous + " Compare to " + e.Text);
                    int order = previous.CompareTo(e.Text);
                    order.Should().Be(-1);
                    previous = e.Text;
                }
            }
        }

        public void VerfiyDescending()
        {
            IList<IWebElement> Payees = getPayees();
            string previous = "";
            foreach (var e in Payees)
            {
                if (previous == "")
                {
                    previous = e.Text;
                }
                else
                {
                    int order = previous.CompareTo(e.Text);
                    order.Should().Be(1);
                }
            }
        }

        public void ResordedByName()
        {
            btnSortName.Click();
        }

        public IList<IWebElement> getPayees()
        {
            IList<IWebElement> Payees = _driver.FindElements(By.XPath("//span[@class='js-payee-name']"));
            return Payees;
        }


        private IWebElement btnAdd => Extensions.WaitForVisible(_driver, By.XPath("//button/span[contains(.,'Add')]"));
        private IWebElement btnAddPayee => _driver.FindElement(By.XPath("//div[@class='row controls']/button[contains(.,'Add')]"));
        private IWebElement txtPayeeName => _driver.FindElement(By.XPath("//input[@id='ComboboxInput-apm-name']"));
        private IWebElement ddlNewPayee => _driver.FindElement(By.XPath("//span[contains(@title,'Someone new:')]"));
        private IWebElement txtBank => _driver.FindElement(By.XPath("//div[@class='js-account-number-wrapper row account-row']/input[1]"));
        private IWebElement txtBranch => _driver.FindElement(By.XPath("//div[@class='js-account-number-wrapper row account-row']/input[2]"));
        private IWebElement txtAccount => _driver.FindElement(By.XPath("//div[@class='js-account-number-wrapper row account-row']/input[3]"));
        private IWebElement txtSuffix => _driver.FindElement(By.XPath("//div[@class='js-account-number-wrapper row account-row']/input[4]"));
        private IWebElement lblNotification => _driver.FindElement(By.XPath("//div[@id='notification']/div/span[@class='message']"));
        private IWebElement btnSortName => _driver.FindElement(By.XPath("//span[contains(.,'Name')]"));
    }
}
