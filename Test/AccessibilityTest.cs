using Deque.AxeCore.Commons;
using final.Core.Helper;
using final.Page;

namespace final.Test
{
    public class AccessibilityTest : BaseTest
    {
        private LoginPage _loginPage;
        private FormPage _formPage;

        [SetUp]
        public void PageSetup()
        {
            _loginPage = new LoginPage();
            _formPage = new FormPage();
        }

        [Test, Description("Scan Accessibility for Registration Form (partial page) and Form Page (full page)")]
        public void TC_ScanAccessibilityForRegistrationFormAndFormPage()
        {
            ExtentReportHelper.LogTestStep("1. Go to Form Page");
            DriverHelper.GoToUrl(FORM_URL);

            ExtentReportHelper.LogTestStep("2. Scan Accessibility for the Registration Form");
            AxeResult formAccessibilityResult = AxeHelper.ScanElementAccessibility(_formPage.getRegistrationForm(), "RegistrationForm");

            ExtentReportHelper.LogTestStep("3. Verify that the Registration Form has no accessibility violation");
            Assert.That(formAccessibilityResult.Violations.Count, Is.EqualTo(0), "Accessibility violations found, for more detail please see the HTML report file.");

            ExtentReportHelper.LogTestStep("4. Scan Accessibility for the whole Form Page");
            AxeResult pageAccessibilityResult = AxeHelper.ScanPageAccessibility("FormPage");

            ExtentReportHelper.LogTestStep("5. Verify that the Form Page has no accessibility violation");
            Assert.That(pageAccessibilityResult.Violations.Count, Is.EqualTo(0), "Accessibility violations found, for more detail please see the HTML report file.");
        }

        [Test, Description("Scan Accessibility for Book Store Page with default options")]
        public void TC_ScanAccessibilityForBookStorePageWithDefaultOptions()
        {
            ExtentReportHelper.LogTestStep("1. Go to Book Store Page");
            DriverHelper.GoToUrl(BOOKSTORE_URL);

            ExtentReportHelper.LogTestStep("2. Scan Accessibility for the whole Book Store Page");
            AxeResult pageAccessibilityResult = AxeHelper.ScanPageAccessibility("BookStorePage");

            ExtentReportHelper.LogTestStep("3. Verify that the Book Store Page has no accessibility violation");
            Assert.That(pageAccessibilityResult.Violations.Count, Is.EqualTo(0), "Accessibility violations found, for more detail please see the HTML report file.");
        }

        [Test, Description("Scan Accessibility for Login Page and Profile Page with custom options")]
        public void TC_ScanAccessibilityForLoginPageAndProfilePageWithCustomOptions()
        {
            ExtentReportHelper.LogTestStep("0. Define the scan options for Axe");
            var options = new AxeRunOptions
            {
                RunOnly = new RunOnlyOptions
                {
                    Type = "tag",
                    Values = new List<string> { "wcag2aa", "wcag21aa" }
                },
                ResultTypes = new HashSet<ResultType> { ResultType.Violations, ResultType.Incomplete },
                Rules = new Dictionary<string, RuleOptions>
                {
                    ["color-contrast"] = new RuleOptions { Enabled = false },
                    ["image-alt"] = new RuleOptions { Enabled = true }
                },
            };

            ExtentReportHelper.LogTestStep("1. Go to Login Page");
            DriverHelper.GoToUrl(LOGIN_URL);

            ExtentReportHelper.LogTestStep("2. Scan Accessibility for the whole Login Page");
            AxeResult loginPageAccessibilityResult = AxeHelper.ScanPageAccessibility("LoginPage", options);

            ExtentReportHelper.LogTestStep("3. Verify that the Login Page has no accessibility violation");
            Assert.That(loginPageAccessibilityResult.Violations.Count, Is.EqualTo(0), "Accessibility violations found, for more detail please see the HTML report file.");

            ExtentReportHelper.LogTestStep("4. Login with valid account");
            _loginPage.login(Constant.USERNAME, Constant.PASSWORD);

            ExtentReportHelper.LogTestStep("5. Scan Accessibility for the whole Profile Page");
            AxeResult profilePageAccessibilityResult = AxeHelper.ScanPageAccessibility("ProfilePage", options);

            ExtentReportHelper.LogTestStep("6. Verify that the Profile Page has no accessibility violation");
            Assert.That(profilePageAccessibilityResult.Violations.Count, Is.EqualTo(0), "Accessibility violations found, for more detail please see the HTML report file.");
        }
    }
}