using TWP.Selenium.Axe.Html;
using Deque.AxeCore.Commons;
using Deque.AxeCore.Selenium;

namespace final.Core.Helper
{
    public static class AxeHelper
    {
        public static AxeResult ScanPageAccessibility(string pageName, AxeRunOptions options = null)
        {
            AxeBuilder axeBuilder = new AxeBuilder(DriverManager.WebDriver);
            if (options != null)
            {
                axeBuilder.WithOptions(options);
            }
            AxeResult axeResult = axeBuilder.Analyze();

            string resultFolderPath = Directory.GetCurrentDirectory() + "\\..\\..\\..\\TestResult";
            if (!Directory.Exists(resultFolderPath))
            {
                Directory.CreateDirectory(resultFolderPath);
            }
            string reportHtmlPath = Path.Combine(resultFolderPath, $"AxeReport_Page_{pageName}.html");
            DriverManager.WebDriver.CreateAxeHtmlReport(axeResult, reportHtmlPath, ReportTypes.Passes | ReportTypes.Violations | ReportTypes.Incomplete);

            return axeResult;
        }

        public static AxeResult ScanElementAccessibility(Element element, string elementName, AxeRunOptions options = null)
        {
            AxeBuilder axeBuilder = new AxeBuilder(DriverManager.WebDriver);
            if (options != null)
            {
                axeBuilder.WithOptions(options);
            }
            AxeResult axeResult = axeBuilder.Analyze(element.WaitForElementToBeVisible());

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
