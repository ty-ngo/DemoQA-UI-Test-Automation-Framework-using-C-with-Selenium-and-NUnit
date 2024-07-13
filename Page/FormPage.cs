using AventStack.ExtentReports.Utils;
using final.Core;
using final.Models;
using OpenQA.Selenium;
using System.IO.Enumeration;

namespace final.Page
{
    public class FormPage : BasePage
    {
        public FormPage() { }

        public Element _txtFirstName = new Element(By.Id("firstName"));
        public Element _txtLastName = new Element(By.Id("lastName"));
        public Element _txtEmail = new Element(By.Id("userEmail"));
        public Element _lblGender(string gender) { return new Element(By.XPath($"//label[text()='{gender}']")); }
        public Element _txtMobile = new Element(By.Id("userNumber"));
        public Element _txtDateOfBirth = new Element(By.Id("dateOfBirthInput"));
        public Element _rdoYear = new Element(By.XPath("//select[contains(@class,'year-select')]"));
        public Element _rdoMonth = new Element(By.XPath("//select[contains(@class,'month-select')]"));
        public Element _btnDay(string targetDay) { return new Element(By.XPath($"//div[text()='{targetDay}' and not(contains(@class,'outside-month'))]")); }
        public Element _txtSubject = new Element(By.Id("subjectsInput"));
        public Element _inpPicture = new Element(By.Id("uploadPicture"));
        public Element _txaCurrentAddress = new Element(By.Id("currentAddress"));
        public Element _cboState = new Element(By.CssSelector("#state input"));
        public Element _cboCity = new Element(By.CssSelector("#city input"));
        public Element _btnSubmit = new Element(By.Id("submit"));
        public Element _lblThankYouMessage = new Element(By.XPath("//div[contains(@class,'modal-title')]"));

        public IList<Element> _lblHobbyOptions(string[] hobbies)
        {
            List<Element> hobbiesElementsList = new List<Element>();
            foreach (string hobby in hobbies)
            {
                hobbiesElementsList.Add(new Element(By.XPath($"//label[text()='{hobby}']")));
            }
            return hobbiesElementsList;
        }

        public Element _lblResultInfo(string fieldName)
        {
            return new Element(By.XPath($"//td[text()='{fieldName}']/following-sibling::td"));
        }

        public void inputFirstName(string firstName)
        {
            if (!firstName.IsNullOrEmpty())
            {
                _txtFirstName.Enter(firstName);
            }
        }

        public void inputLastName(string lastName)
        {
            if (!lastName.IsNullOrEmpty())
            {
                _txtLastName.Enter(lastName);
            }
        }

        public void inputEmail(string email)
        {
            if (!email.IsNullOrEmpty())
            {
                _txtEmail.Enter(email);
            }
        }

        public void selectGender(string gender)
        {
            if (!gender.IsNullOrEmpty())
            {
                _lblGender(gender).ClickByJS();
            }
        }

        public void inputMobile(string mobile)
        {
            if (!mobile.IsNullOrEmpty())
            {
                _txtMobile.Enter(mobile);
            }
        }

        public void inputDateOfBirth(string dateOfBirth)
        {
            string[] split = dateOfBirth.Split([' ', '-']);
            _txtDateOfBirth.Click();
            _rdoYear.Select(split[2]);
            _rdoMonth.Select(split[1]);
            _btnDay(split[0]).Click();
        }

        public void inputSubject(string[] subjects)
        {
            if (!subjects.IsNullOrEmpty())
            {
                foreach (string subject in subjects)
                {
                    _txtSubject.Enter(subject);
                    _txtSubject.Enter(Keys.Enter);
                }
            }
        }

        public void selectHobbies(string[] hobbies)
        {
            if (!hobbies.IsNullOrEmpty())
            {
                foreach (Element _lblHobbyOption in _lblHobbyOptions(hobbies))
                {
                    _lblHobbyOption.ClickByJS();
                }
            }
        }

        public void uploadPicture(string path)
        {
            if (!path.IsNullOrEmpty())
            {
                _inpPicture.Enter(Directory.GetCurrentDirectory() + path);
            }
        }

        public void inputCurrentAddress(string address)
        {
            if (!address.IsNullOrEmpty())
            {
                _txaCurrentAddress.Enter(address);
            }
        }

        public void selectState(string state)
        {
            if (!state.IsNullOrEmpty())
            {
                _cboState.Enter(state);
                _cboState.Enter(Keys.Enter);
            }
        }

        public void selectCity(string city)
        {
            if (!city.IsNullOrEmpty())
            {
                _cboCity.Enter(city);
                _cboCity.Enter(Keys.Enter);
            }
        }

        public void clickSubmitButton()
        {
            _btnSubmit.Click();
        }

        public void registerStudent(Student student)
        {
            inputFirstName(student.firstName);
            inputLastName(student.lastName);
            inputEmail(student.email);
            selectGender(student.gender);
            inputMobile(student.mobileNumber);
            inputDateOfBirth(student.dateOfBirth);
            inputSubject(student.subjects);
            selectHobbies(student.hobbies);
            uploadPicture(student.picturePath);
            inputCurrentAddress(student.currentAddress);
            selectState(student.state);
            selectCity(student.city);
            clickSubmitButton();
        }

        public bool checkThankYouMessage()
        {
            return (_lblThankYouMessage.GetText() == "Thanks for submitting the form");
        }

        public bool checkNameResult(string expectedFirstName, string expectedLastName)
        {
            return (_lblResultInfo("Student Name").GetText() == $"{expectedFirstName} {expectedLastName}");
        }

        public bool checkEmailResult(string expectedValue)
        {
            return (_lblResultInfo("Student Email").GetText() == expectedValue);
        }

        public bool checkGenderResult(string expectedValue)
        {
            return (_lblResultInfo("Gender").GetText() == expectedValue);
        }

        public bool checkMobileResult(string expectedValue)
        {
            return (_lblResultInfo("Mobile").GetText() == expectedValue);
        }

        public bool checkDateOfBirthResult(string expectedValue)
        {
            string actualValue = _lblResultInfo("Date of Birth").GetText().Replace(',', ' ');
            return (actualValue == expectedValue);
        }

        public bool checkSubjectsResult(string[] expectedList)
        {
            string actual = _lblResultInfo("Subjects").GetText();

            if (expectedList.IsNullOrEmpty() && actual.IsNullOrEmpty())
            {
                return true;
            }

            else if ((!expectedList.IsNullOrEmpty() && actual.IsNullOrEmpty()) ||
                     (expectedList.IsNullOrEmpty() && !actual.IsNullOrEmpty()))
            {
                return false;
            }

            else
            {
                string[] actualList = actual.Split(", ");

                if (actualList.Length != expectedList.Length)
                {
                    return false;
                }

                Array.Sort(actualList);
                Array.Sort(expectedList);

                for (int i = 0; i < actualList.Length; i++)
                {
                    if (!actualList[i].Equals(expectedList[i]))
                        return false;
                }

                return true;
            }
        }

        public bool checkHobbiesResult(string[] expectedList)
        {
            string actual = _lblResultInfo("Hobbies").GetText();

            if (expectedList.IsNullOrEmpty() && actual.IsNullOrEmpty())
            {
                return true;
            }

            else if ((!expectedList.IsNullOrEmpty() && actual.IsNullOrEmpty()) ||
                     (expectedList.IsNullOrEmpty() && !actual.IsNullOrEmpty()))
            {
                return false;
            }

            else
            {
                string[] actualList = actual.Split(", ");

                if (actualList.Length != expectedList.Length)
                {
                    return false;
                }

                Array.Sort(actualList);
                Array.Sort(expectedList);

                for (int i = 0; i < actualList.Length; i++)
                {
                    if (!actualList[i].Equals(expectedList[i]))
                        return false;
                }

                return true;
            }
        }

        public bool checkPictureResult(string expectedValue)
        {
            string[] pathSplit = expectedValue.Split("\\");
            string fileName = pathSplit[pathSplit.Length - 1];
            return (_lblResultInfo("Picture").GetText() == fileName);
        }

        public bool checkAddressResult(string expectedValue)
        {
            return (_lblResultInfo("Address").GetText() == expectedValue);
        }

        public bool checkStateCityResult(string expectedState, string expectedCity)
        {
            if (expectedState.IsNullOrEmpty())
            {
                return (_lblResultInfo("State and City").GetText() == "");
            }

            else if (expectedCity.IsNullOrEmpty())
            {
                return (_lblResultInfo("State and City").GetText() == expectedState);
            }

            else
            {
                return (_lblResultInfo("State and City").GetText() == $"{expectedState} {expectedCity}");
            }
        }

        public bool checkStudentInfo(Student student)
        {
            return (checkNameResult(student.firstName, student.lastName) &&
                    checkEmailResult(student.email) &&
                    checkGenderResult(student.gender) &&
                    checkMobileResult(student.mobileNumber) &&
                    checkDateOfBirthResult(student.dateOfBirth) &&
                    checkSubjectsResult(student.subjects) &&
                    checkHobbiesResult(student.hobbies) &&
                    checkPictureResult(student.picturePath) &&
                    checkAddressResult(student.currentAddress) &&
                    checkStateCityResult(student.state, student.city));
        }
    }
}
