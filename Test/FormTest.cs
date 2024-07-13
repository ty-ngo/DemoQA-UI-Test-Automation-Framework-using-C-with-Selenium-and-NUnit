using final.Core;
using final.Core.Helper;
using final.Models;
using final.Page;

namespace final.Test
{
    public class FormTest:BaseTest
    {
        private FormPage _formPage;

        [SetUp]
        public void PageSetup()
        {
            _formPage = new FormPage();
        }

        [Test, Description("Register student form with all fields successfully")]
        public void TC_RegisterStudentFormWithAllFieldsSuccessfully()
        {
            ExtentReportHelper.LogTestStep("1. Go to form page");
            DriverHelper.GoToUrl(FORM_URL);

            ExtentReportHelper.LogTestStep("2. Read student data from json");
            Student student = JsonHelper.LoadStudentJson("TestData\\Json\\StudentAllFields.json");

            ExtentReportHelper.LogTestStep("3. Register student");
            _formPage.registerStudent(student);

            bool checkMessage = _formPage.checkThankYouMessage();
            bool checkStudentInfo = _formPage.checkStudentInfo(student);

            ExtentReportHelper.LogTestStep("4. Verify the Thank You label");
            Assert.That(checkMessage, Is.EqualTo(true), "Failed due to Thank You label not found");

            ExtentReportHelper.LogTestStep("5. Verify the student info");
            Assert.That(checkStudentInfo, Is.EqualTo(true), "Failed due to incorrect student info");
        }

        [Test, Description("Register student form with mandatory fields successfully")]
        public void TC_RegisterStudentFormWithMandatoryFieldsSuccessfully()
        {
            ExtentReportHelper.LogTestStep("1. Go to form page");
            DriverHelper.GoToUrl(FORM_URL);

            ExtentReportHelper.LogTestStep("2. Read student data from json");
            Student student = JsonHelper.LoadStudentJson("TestData\\Json\\StudentMandatoryFields.json");

            ExtentReportHelper.LogTestStep("3. Register student");
            _formPage.registerStudent(student);

            bool checkMessage = _formPage.checkThankYouMessage();
            bool checkStudentInfo = _formPage.checkStudentInfo(student);

            ExtentReportHelper.LogTestStep("4. Verify the Thank You label");
            Assert.That(checkMessage, Is.True, "Failed due to Thank You label not found");

            ExtentReportHelper.LogTestStep("5. Verify the student info");
            Assert.That(checkStudentInfo, Is.True, "Failed due to incorrect student info");
        }
    }
}
