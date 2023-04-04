using System;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UIAutomation.Utilities
{
    public static class Extensions
    {
        public static IWebElement WaitForEnabled(this IWebElement element, int timeSpan = 5000)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            while (watch.Elapsed.Milliseconds < timeSpan)
            {
                if (element.Enabled)
                    return element;
            }

            throw new ElementNotInteractableException();
        }

        public static IWebElement WaitForVisible(this IWebElement element, int timeSpan = 5000)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            while (watch.Elapsed.Milliseconds < timeSpan)
            {
                if (element.Displayed)
                    return element;
            }

            throw new ElementNotVisibleException();
        }

        public static IWebElement WaitForVisible(IWebDriver driver, By locator, int timeSpan = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeSpan));
                return wait.Until(drv => drv.FindElement(locator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + locator + "' was not found in current context page.");
                throw;
            }
        }

        public static IWebElement WaitForClickable(IWebDriver driver, By locator, int timeSpan = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeSpan));
                return wait.Until(drv => drv.FindElement(locator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + locator + "' was not clickable in current context page.");
                throw;
            }
        }

        public static IWebElement WaitForText(this IWebElement element, int timeSpan = 5000)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            while (watch.Elapsed.Milliseconds < timeSpan)
            {
                if (element.Text.Length > 0)
                    return element;
            }

            throw new ElementNotVisibleException();
        }

        public static IWebElement WaitForText(this IWebElement element, string text, int timeSpan = 5000)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            while (watch.Elapsed.Seconds < timeSpan)
            {
                if (element.Text == text)
                    return element;
            }

            throw new NoSuchElementException();
        }
    }
}
