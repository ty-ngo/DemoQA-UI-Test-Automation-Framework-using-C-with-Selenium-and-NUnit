using OpenQA.Selenium;
using final.Core;

namespace final.Page
{
    public abstract class BasePage
    {
        protected BasePage() { }

        private Element _btnLogOut = new Element(By.XPath("//button[text()='Log out']"));
        private Element _txtSearchBox = new Element(By.Id("searchBox"));

        public void SearchBooks(string searchValue)
        {
            _txtSearchBox.Enter(searchValue);
        }

        public bool CheckLoggedIn()
        {
            if (_btnLogOut.WaitForElementToBeVisible() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Logout()
        {
            _btnLogOut.Click();
        }
    }
}
