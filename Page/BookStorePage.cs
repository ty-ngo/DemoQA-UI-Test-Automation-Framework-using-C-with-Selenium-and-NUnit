using final.Core;
using OpenQA.Selenium;

namespace final.Page
{
    public class BookStorePage:BasePage
    {

        private By _lblResultsInColumn(string columnName)
        {
            if (columnName == "Title")
            {
                return By.XPath($"//div[not(contains(@class,'-padRow'))]/div[@role='gridcell'][count(//div[@role='columnheader' and .='{columnName}']/preceding-sibling::div)+1]//a");
            }

            else if (columnName == "Author" || columnName == "Publisher")
            {
                return By.XPath($"//div[not(contains(@class,'-padRow'))]/div[@role='gridcell'][count(//div[@role='columnheader' and .='{columnName}']/preceding-sibling::div)+1]");
            }
            
            else
            {
                return null;
            }
        }

        public bool checkSearchResult(string searchValue)
        {
            IList<IWebElement> titleList = DriverManager.WebDriver.FindElements(_lblResultsInColumn("Title"));
            IList<IWebElement> authorList = DriverManager.WebDriver.FindElements(_lblResultsInColumn("Author"));
            IList<IWebElement> publisherList = DriverManager.WebDriver.FindElements(_lblResultsInColumn("Publisher"));

            for (int i = 0; i < titleList.Count; i++)
            {
                if (!titleList[i].Text.ToLower().Trim().Contains(searchValue.ToLower().Trim()) &&
                    !authorList[i].Text.ToLower().Trim().Contains(searchValue.ToLower().Trim()) &&
                    !publisherList[i].Text.ToLower().Trim().Contains(searchValue.ToLower().Trim()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
