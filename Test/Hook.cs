using final.Core;
using final.Core.Helper;

namespace final.Test
{
    [SetUpFixture]
    public class Hook
    {
        const string AppSettingPath = "Configurations\\appsettings.json";

        [OneTimeSetUp]
        public void MySetup()
        {
            TestContext.Progress.WriteLine("===> Global one time setup");

            var config = ConfigurationHelper.ReadConfiguration(AppSettingPath);
            string enviroment = config["enviroment"];
            string browser = config["browser"];
            string date = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            ExtentReportHelper.InitializeReport($"TestResult\\Result-{date}\\result.html", "Hostname", enviroment, browser);
        }


        [OneTimeTearDown]
        public void End()
        {
            
        }
    }
}