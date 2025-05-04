using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using TWP.Selenium.Axe.Html;
using Deque.AxeCore.Commons;
using Deque.AxeCore.Selenium;
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

        public static AxeResult ScanPageAccessibility(string pageName)
        {
            AxeBuilder axeBuilder = new AxeBuilder(DriverManager.WebDriver);
            AxeResult axeResult = axeBuilder.WithTags("wcag2a", "wcag2aa", "wcag21a", "wcag21aa", "wcag21aa").Analyze();

            string resultFolderPath = Directory.GetCurrentDirectory() + "\\..\\..\\..\\TestResult";
            if (!Directory.Exists(resultFolderPath))
            {
                Directory.CreateDirectory(resultFolderPath);
            }
            string reportHtmlPath = Path.Combine(resultFolderPath, $"AxeReport_Page_{pageName}.html");
            DriverManager.WebDriver.CreateAxeHtmlReport(axeResult, reportHtmlPath, ReportTypes.Passes | ReportTypes.Violations | ReportTypes.Incomplete);

            return axeResult;
        }

        public static AxeResult ScanElementAccessibility(Element element, string elementName)
        {
            AxeBuilder axeBuilder = new AxeBuilder(DriverManager.WebDriver);
            AxeResult axeResult = axeBuilder.WithTags("wcag2a", "wcag2aa", "wcag21a", "wcag21aa", "wcag21aa").Analyze(element.WaitForElementToBeVisible());

            string resultFolderPath = Directory.GetCurrentDirectory() + "\\..\\..\\..\\TestResult";
            if (!Directory.Exists(resultFolderPath))
            {
                Directory.CreateDirectory(resultFolderPath);
            }
            string reportHtmlPath = Path.Combine(resultFolderPath, $"AxeReport_Element_{elementName}.html");
            DriverManager.WebDriver.CreateAxeHtmlReport(axeResult, reportHtmlPath, ReportTypes.Passes | ReportTypes.Violations | ReportTypes.Incomplete);

            return axeResult;
        }
    }
}
