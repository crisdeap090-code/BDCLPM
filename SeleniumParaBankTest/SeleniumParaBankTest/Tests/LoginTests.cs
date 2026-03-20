using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumParaBank.Pages;
using SeleniumParaBank.Utilities;
using SeleniumParaBankTest.TestData;
using SeleniumParaBankTest.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumParaBankTest.Tests
{
    public class LoginTests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = DriverFactory.InitDriver();
        }

        [Test]
        public void Run_Login_Tests_From_Json()
        {
            List<UserData> users = JsonDataReader.GetUsers();

            int row = 8;

            // chỉ chạy 10 test case đầu
            foreach (var user in users.Take(10))
            {
                driver.Navigate().GoToUrl("https://parabank.parasoft.com/parabank/logout.htm");
                driver.Navigate().GoToUrl("https://parabank.parasoft.com/parabank/index.htm");

                LoginPage login = new LoginPage(driver);

                login.Login(user.username, user.password);

                bool result = login.IsLoginSuccess();

                string actual = result ? "Login Success" : "Login Fail";
                string status = result == user.expected ? "PASS" : "FAIL";

                string screenshot = ScreenshotHelper.Capture(driver);

                ExcelHelper.WriteResult(row, actual, status, screenshot);

                row++;
            }
        }

        [TearDown]
        public void Close()
        {
            driver.Quit();
        }
    }
}