namespace final.Core.Helper
{
    public static class DriverHelper
    {
        public static void GoToUrl(string url)
        {
            DriverManager.WebDriver.Url = url;
        }
    }
}
