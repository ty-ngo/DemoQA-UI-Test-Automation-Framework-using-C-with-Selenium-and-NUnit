using AventStack.ExtentReports.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace final.Core
{
    public class Element
    {
        public By By { get; set; }

        public Element(By by)
        {
            By = by;
        }

        public IWebElement WaitForElementToBeVisible(int seconds = 10)
        {
            try
            {
                var wait = new WebDriverWait(DriverManager.WebDriver, TimeSpan.FromSeconds(seconds));
                return wait.Until(ExpectedConditions.ElementIsVisible(By));
            }
            catch (WebDriverTimeoutException)
            {
                return null;
            }
        }

        public IWebElement WaitForElementToBeClickable(int seconds = 10)
        {
            try
            {
                var wait = new WebDriverWait(DriverManager.WebDriver, TimeSpan.FromSeconds(seconds));
                return wait.Until(ExpectedConditions.ElementToBeClickable(By));
            }
            catch (WebDriverTimeoutException)
            {
                return null;
            }
        }

        public void WaitForElementToChange(int seconds = 10)
        {
            try
            {
                var wait = new WebDriverWait(DriverManager.WebDriver, TimeSpan.FromSeconds(seconds));
                wait.Until(ExpectedConditions.StalenessOf(DriverManager.WebDriver.FindElement(By)));
            }
            catch (WebDriverTimeoutException) { }
        }

        public void Click()
        {
            IWebElement element = WaitForElementToBeVisible();
            element.Click();
        }

        public void ClickByJS()
        {
            IWebElement element = WaitForElementToBeVisible();
            IJavaScriptExecutor js = (IJavaScriptExecutor)DriverManager.WebDriver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            element.Click();
        }

        public void Enter(string value)
        {
            IWebElement element = WaitForElementToBeVisible();
            element.SendKeys(value);
        }

        public void Select(string value)
        {
            IWebElement element = WaitForElementToBeVisible();
            SelectElement select = new SelectElement(element);
            select.SelectByText(value);
        }

        public string GetText()
        {
            IWebElement element = WaitForElementToBeVisible();
            return element.Text.Trim();
        }

        public IList<IWebElement> FindAllElements()
        {
            return DriverManager.WebDriver.FindElements(By);
        }

        public bool checkElementExist()
        {
            return (!FindAllElements().IsNullOrEmpty());
        }
    }

}
