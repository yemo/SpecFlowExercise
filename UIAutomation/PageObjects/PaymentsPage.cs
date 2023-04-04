using OpenQA.Selenium;
using UIAutomation.Utilities;
using FluentAssertions;
using TechTalk.SpecFlow.Infrastructure;

namespace UIAutomation.PageObjects
{
    public class PaymentsPage : ClientPage
    {
        private readonly IWebDriver _driver;
        private readonly ISpecFlowOutputHelper _log;
        public PaymentsPage(IWebDriver driver, ISpecFlowOutputHelper log) : base(driver)
        {
            _driver = driver;
            _log = log;
        }

        public string SelectFromAccountAndGetBalance(string name)
        {
            btnFromAccount.Click();
            return SelectAccountAndGetBalance(name);
        }

        public string SelectToAccountAndGetBalance(string name)
        {
            btnToAccount.Click();
            //click Accounts tab
            IWebElement tab = Extensions.WaitForVisible(_driver, By.XPath("//li[@data-testid='to-account-accounts-tab']"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)_driver;
            executor.ExecuteScript("arguments[0].click();", tab);
            return SelectAccountAndGetBalance(name);
        }

        public string SelectAccountAndGetBalance(string name)
        {
            IWebElement row = Extensions.WaitForVisible(_driver, By.XPath("//li/button[./div/div/div/p[contains(.,'" + name + "')]]"));
            string balance = row.FindElement(By.XPath("./div/div/div/p[2]")).Text.Split()[0].Trim(new char[] {' ', '$' });
            IJavaScriptExecutor executor = (IJavaScriptExecutor)_driver;
            executor.ExecuteScript("arguments[0].click();", row);
            return balance;
        }

        public void EnterAmount(string amount)
        {
            txtAmount.Click();
            txtAmount.SendKeys(amount);
        }

        public void MakeTransfer()
        {
            Extensions.WaitForVisible(_driver, By.XPath("//button[@type='submit']/span/span[.='Transfer']")).Click();
        }


        private IWebElement btnFromAccount => _driver.FindElement(By.XPath("//button[@data-monitoring-label='Transfer Form From Chooser']"));
        private IWebElement btnToAccount => _driver.FindElement(By.XPath("//button[@data-monitoring-label='Transfer Form To Chooser']"));
        private IWebElement txtAmount => _driver.FindElement(By.XPath("//input[@name='amount']"));
    }
}
