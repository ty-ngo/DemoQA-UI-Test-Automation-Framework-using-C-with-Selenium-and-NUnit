using final.Core;
using final.Core.Helper;

namespace final.Test
{
    [TestFixture]
    [TestFixture, Parallelizable(ParallelScope.Fixtures)]
    public abstract class BaseTest
    {
        public static string BROWSER = ConfigurationHelper.GetConfiguration()["browser"];
        public static string HOME_URL = ConfigurationHelper.GetConfiguration()["HOME_URL"];
        public static string LOGIN_URL = ConfigurationHelper.GetConfiguration()["LOGIN_URL"];
        public static string FORM_URL = ConfigurationHelper.GetConfiguration()["FORM_URL"];
        public static string BOOKSTORE_URL = ConfigurationHelper.GetConfiguration()["BOOKSTORE_URL"];
        public static string PROFILE_URL = ConfigurationHelper.GetConfiguration()["PROFILE_URL"];
        public static int IMPLICIT_WAIT_SECONDS = int.Parse(ConfigurationHelper.GetConfiguration()["implicit.wait.seconds"]);
        public static int PAGE_LOAD_SECONDS = int.Parse(ConfigurationHelper.GetConfiguration()["page.load.seconds"]);

        public BaseTest()
        {
            ExtentReportHelper.CreateTest(TestContext.CurrentContext.Test.ClassName);
        }

        [SetUp]
        public void Setup()
        {
            ExtentReportHelper.CreateNode(TestContext.CurrentContext.Test.Name);
            ExtentReportHelper.LogTestStep("Initialize webdriver");

            DriverManager.InitDriver(BROWSER);
            DriverManager.WebDriver.Manage().Window.Maximize();
            DriverManager.WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(IMPLICIT_WAIT_SECONDS);
            DriverManager.WebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(PAGE_LOAD_SECONDS);
        }

        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
            ? ""
            : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            ExtentReportHelper.CreateTestResult(status, stacktrace, TestContext.CurrentContext.Test.ClassName, TestContext.CurrentContext.Test.Name, DriverManager.WebDriver);
            ExtentReportHelper.Flush();

            ExtentReportHelper.LogTestStep("Close webdriver");

            DriverManager.CloseDriver();
        }
    }
}
