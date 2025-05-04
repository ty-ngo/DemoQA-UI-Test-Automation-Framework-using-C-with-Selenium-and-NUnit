using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium;

namespace final.Core.Helper
{
    public static class DriverHelper
    {
        public static void GoToUrl(string url)
        {
            DriverManager.WebDriver.Url = url;
        }

        public static void AcceptAlert()
        {
            WebDriverWait wait = new WebDriverWait(DriverManager.WebDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = DriverManager.WebDriver.SwitchTo().Alert();
            alert.Accept();
        }
    }
}
