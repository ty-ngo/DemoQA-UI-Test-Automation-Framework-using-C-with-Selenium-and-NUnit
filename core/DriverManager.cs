using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace final.Core
{
    public static class DriverManager
    {

        [ThreadStatic]
        public static IWebDriver WebDriver;

        public static void InitDriver(string browserName)
        {
            switch (browserName.ToLower())
            {
                case "chrome":
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments("test-type");
                    //chromeOptions.AddArguments("headless");
                    chromeOptions.AddArgument("--no-sandbox");
                    chromeOptions.AddArguments("--start-maximized");
                    WebDriver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), chromeOptions);
                    break;

                case "edge":
                    var edgeOptions = new EdgeOptions();
                    //edgeOptions.AddArguments("headless");
                    edgeOptions.AddArgument("--no-sandbox");
                    WebDriver = new EdgeDriver(edgeOptions);
                    break;

                case "firefox":
                    var firefoxOptions = new FirefoxOptions();
                    //firefoxOptions.AddArguments("headless");
                    WebDriver = new FirefoxDriver(firefoxOptions);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(browserName);
            }
        }

        public static void CloseDriver()
        {
            WebDriver.Quit();
        }
    }
}
