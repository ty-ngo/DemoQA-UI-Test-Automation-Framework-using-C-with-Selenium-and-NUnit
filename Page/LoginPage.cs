using final.Core;
using OpenQA.Selenium;

namespace final.Page
{
    public class LoginPage : BasePage
    {
        private Element _txtUsername = new Element(By.Id("userName"));
        private Element _txtPassword = new Element(By.Id("password"));
        private Element _btnLogin = new Element(By.Id("login"));
        private Element _btnRegister = new Element(By.Id("newUser"));

        public LoginPage() { }

        public void login(string username, string password)
        {
            _txtUsername.Enter(username);
            _txtPassword.Enter(password);
            _btnLogin.ClickByJS();
            Assert.That(CheckLoggedIn(), Is.True, "Login failed.");
        }
    }
}
