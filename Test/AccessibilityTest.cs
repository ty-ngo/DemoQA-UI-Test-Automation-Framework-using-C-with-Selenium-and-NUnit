using Deque.AxeCore.Commons;
using final.Core.Helper;
using final.Page;

namespace final.Test
{
    public class AccessibilityTest : BaseTest
    {
        private BookStorePage _bookStorePage;
        private LoginPage _loginPage;
        private ProfilePage _profilePage;
        private FormPage _formPage;

        [SetUp]
        public void PageSetup()
        {
            _bookStorePage = new BookStorePage();
            _loginPage = new LoginPage();
            _profilePage = new ProfilePage();
            _formPage = new FormPage();
        }

        [Test, Description("Scan Accessibility for Form Page and Registration Form")]
        public void TC_ScanAccessibilityForRegistrationPageAndRegistrationForm()
        {
            ExtentReportHelper.LogTestStep("1. Go to Form Page");
            DriverHelper.GoToUrl(FORM_URL);

            ExtentReportHelper.LogTestStep("2. Scan Accessibility for the Registration Form");
            AxeResult formAccessibilityResult = DriverHelper.ScanElementAccessibility(_formPage.getRegistrationForm(), "RegistrationForm");

            ExtentReportHelper.LogTestStep("3. Verify that the Registration Form has no accessibility violation");
            Assert.That(formAccessibilityResult.Violations.Count, Is.EqualTo(0), "Accessibility violations found, for more detail please see the HTML report file.");

            ExtentReportHelper.LogTestStep("4. Scan Accessibility for the whole Form Page");
            AxeResult pageAccessibilityResult = DriverHelper.ScanPageAccessibility("FormPage");

            ExtentReportHelper.LogTestStep("5. Verify that the Form Page has no accessibility violation");
            Assert.That(pageAccessibilityResult.Violations.Count, Is.EqualTo(0), "Accessibility violations found, for more detail please see the HTML report file.");
        }

        [Test, Description("Scan Accessibility for Book Store Page")]
        public void TC_ScanAccessibilityForBookStorePage()
        {
            ExtentReportHelper.LogTestStep("1. Go to Book Store Page");
            DriverHelper.GoToUrl(BOOKSTORE_URL);

            ExtentReportHelper.LogTestStep("2. Scan Accessibility for the whole Book Store Page");
            AxeResult pageAccessibilityResult = DriverHelper.ScanPageAccessibility("BookStorePage");

            ExtentReportHelper.LogTestStep("3. Verify that the Book Store Page has no accessibility violation");
            Assert.That(pageAccessibilityResult.Violations.Count, Is.EqualTo(0), "Accessibility violations found, for more detail please see the HTML report file.");
        }

        [Test, Description("Scan Accessibility for Login Page and Profile Page")]
        public void TC_ScanAccessibilityForLoginPage()
        {
            ExtentReportHelper.LogTestStep("1. Go to Login Page");
            DriverHelper.GoToUrl(LOGIN_URL);

            ExtentReportHelper.LogTestStep("2. Scan Accessibility for the whole Login Page");
            AxeResult loginPageAccessibilityResult = DriverHelper.ScanPageAccessibility("LoginPage");

            ExtentReportHelper.LogTestStep("3. Verify that the Login Page has no accessibility violation");
            Assert.That(loginPageAccessibilityResult.Violations.Count, Is.EqualTo(0), "Accessibility violations found, for more detail please see the HTML report file.");

            ExtentReportHelper.LogTestStep("4. Login with valid account");
            _loginPage.login(Constant.USERNAME, Constant.PASSWORD);

            ExtentReportHelper.LogTestStep("5. Scan Accessibility for the whole Profile Page");
            AxeResult profilePageAccessibilityResult = DriverHelper.ScanPageAccessibility("ProfilePage");

            ExtentReportHelper.LogTestStep("6. Verify that the Profile Page has no accessibility violation");
            Assert.That(profilePageAccessibilityResult.Violations.Count, Is.EqualTo(0), "Accessibility violations found, for more detail please see the HTML report file.");
        }
    }
}