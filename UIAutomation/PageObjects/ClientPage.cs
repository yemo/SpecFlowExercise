using OpenQA.Selenium;
using UIAutomation.Utilities;


namespace UIAutomation.PageObjects
{
    public class ClientPage
    {
        private readonly IWebDriver _driver;

        public ClientPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void LaunchPage()
        {
            _driver.Navigate().GoToUrl("https://demo.bnz.co.nz/client/");
            Extensions.WaitForVisible(_driver, By.XPath("//div[@class='hints']"));
        }

        public void ClickMenu()
        {
            btnMenu.Click();
            Extensions.WaitForVisible(_driver, By.XPath("//div[@class='MainMenu-bnzIcon']"));
        }

        public void ClickPayee()
        {
            Extensions.WaitForVisible(_driver, By.XPath("//a[@href='/client/payees']")).Click();
            Extensions.WaitForVisible(_driver, By.XPath("//h1[@class='CustomPage-heading']/span"));
        }

        public void ClickPayment()
        {
            Extensions.WaitForVisible(_driver, By.XPath("//button[contains(.,'Pay or transfer')]")).Click();
            Extensions.WaitForVisible(_driver, By.XPath("//form[@id='paymentForm']"));
        }

        public string GetAccountBalance(string name)
        {
            IWebElement AccountInfo = Extensions.WaitForVisible(_driver, By.XPath("//div[./span/h3[contains(@title,'" + name + "')]]"));
            return AccountInfo.FindElement(By.XPath("./span[@class='account-balance']")).Text;
        }

        public string GetMessage()
        {
            IWebElement shows = Extensions.WaitForVisible(_driver, By.XPath("//div[@class='inner js-notification show js-notificationShown']"));
            return shows.FindElement(By.XPath("./span")).Text;
        }



        public IWebElement btnMenu => _driver.FindElement(By.XPath("//button[@tabindex='1']"));
        public IWebElement lblTitle => _driver.FindElement(By.XPath("//h1[@class='CustomPage-heading']/span"));
    }
}
