using final.Core;
using final.Core.Helper;
using final.Page;

namespace final.Test
{
    public class BookStoreTest:BaseTest
    {
        private BookStorePage _bookStorePage;
        private LoginPage _loginPage;
        private ProfilePage _profilePage;

        [SetUp]
        public void PageSetup()
        {
            _bookStorePage = new BookStorePage();
            _loginPage = new LoginPage();
            _profilePage = new ProfilePage();
        }

        [Test, Description("Search book with multiple results")]
        public void TC_SearchBooksWithMultipleResults()
        {
            ExtentReportHelper.LogTestStep("1. Go to bookstore page");
            DriverHelper.GoToUrl(BOOKSTORE_URL);

            ExtentReportHelper.LogTestStep("2. Search book by given value");
            string searchValue = "design";
            _bookStorePage.SearchBooks(searchValue);

            ExtentReportHelper.LogTestStep("3. Verify the search result");
            bool check = _bookStorePage.checkSearchResult(searchValue);
            Assert.That(check, Is.True);
        }

        [Test, Description("Delete book successfully")]
        public void TC_DeleteBookSuccessfully()
        {
            ExtentReportHelper.LogTestStep("1. Go to login page");
            DriverHelper.GoToUrl(LOGIN_URL);

            ExtentReportHelper.LogTestStep("2. Login with valid account");
            _loginPage.login(Constant.USERNAME, Constant.PASSWORD);

            ExtentReportHelper.LogTestStep("3. Search book by given title");
            string bookTitle = "Git Pocket Guide";
            _bookStorePage.SearchBooks(bookTitle);

            ExtentReportHelper.LogTestStep("4. Delete target book");
            _profilePage.deleteBook(bookTitle);

            ExtentReportHelper.LogTestStep("5. Check if the deleted book disappeared");
            bool check = _profilePage.checkBookExist(bookTitle);
            Assert.That(check, Is.EqualTo(false));
        }
    }
}