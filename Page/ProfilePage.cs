using AventStack.ExtentReports.Utils;
using final.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace final.Page
{
    public class ProfilePage:BasePage
    {

        private Element _lnkLogin = new Element(By.XPath("//a[text()='login']"));

        private Element _lblBookTitle(string bookTitle)
        {
            return new Element(By.XPath($"//a[text()='{bookTitle}']"));
        }

        private Element _btnDelete(string bookTitle)
        {
            return new Element(By.XPath($"//a[text()='{bookTitle}']/ancestor::div[@role='row']//span[contains(@id,'delete')]"));
        }

        private Element _btnOK = new Element(By.XPath("//button[text()='OK']"));

        public void clickLoginLink()
        {
            _lnkLogin.Click();
        }

        public void closeAlert()
        {
            WebDriverWait wait = new WebDriverWait(DriverManager.WebDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = DriverManager.WebDriver.SwitchTo().Alert();
            alert.Accept();
        }

        public void deleteBook(string bookTitle)
        {
            if (checkBookExist(bookTitle))
            {
                _btnDelete(bookTitle).Click();
                _btnOK.Click();
                closeAlert();
            }
        }

        public bool checkBookExist(string bookTitle)
        {
            return (_lblBookTitle(bookTitle).WaitForElementToBeVisible() != null);
        }
    }
}
