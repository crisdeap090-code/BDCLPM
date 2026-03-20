using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumParaBank.Pages;
using SeleniumParaBank.Utilities;
using SeleniumParaBankTest.Utilities;
using SeleniumParaBankTest.TestData;
using System.Linq;

namespace SeleniumParaBankTest.Tests
{
    public class RegisterTests
    {
        IWebDriver? driver;

        [SetUp]
        public void Setup()
        {
            driver = DriverFactory.InitDriver();
            driver.Navigate().GoToUrl("https://parabank.parasoft.com/parabank/index.htm");
        }

        [Test]
        public void Run_Register_Tests_From_Json()
        {
            var data = JsonDataReader.GetRegisterData();

            int row = 7;

            // chạy 10 test case đầu tiên
            foreach (var item in data.Take(10))
            {
                try
                {
                    RegisterPage page = new RegisterPage(driver);

                    page.OpenPage();

                    page.FillForm(
                        item.firstName,
                        item.lastName,
                        item.address,
                        item.city,
                        item.state,
                        item.zipCode,
                        item.phone,
                        item.ssn,
                        item.username,
                        item.password,
                        item.confirmPassword
                    );

                    page.ClickRegister();

                    bool result = page.IsRegisterSuccess();

                    string actual = result ? "Register Success" : "Register Fail";

                    string status = (result == item.expected) ? "PASS" : "FAIL";

                    string screenshot = ScreenshotHelper.Capture(driver);

                    ExcelHelper.WriteResult(row, actual, status, screenshot);
                }
                catch
                {
                    ExcelHelper.WriteResult(row, "ERROR", "FAIL", "");
                }

                // mỗi test case trong excel cách nhau 11 dòng
                row ++;
            }
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}