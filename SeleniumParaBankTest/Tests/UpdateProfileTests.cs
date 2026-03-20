using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumParaBank.Pages;
using SeleniumParaBank.Utilities;
using SeleniumParaBankTest.Utilities;
using SeleniumParaBankTest.TestData;
using System.Linq;

namespace SeleniumParaBankTest.Tests
{
    public class UpdateProfileTests
    {
        IWebDriver? driver;

        [SetUp]
        public void Setup()
        {
            driver = DriverFactory.InitDriver();

            driver.Navigate().GoToUrl("https://parabank.parasoft.com/parabank/index.htm");

            LoginPage login = new LoginPage(driver);
            login.Login("ThizKi3u", "19032022");
        }

        [Test]
        public void Run_UpdateProfile_Tests_From_Json()
        {
            var data = JsonDataReader.GetUpdateProfileData();

            int row = 7;

            foreach (var item in data.Take(10))
            {
                try
                {
                    UpdateProfilePage page = new UpdateProfilePage(driver);

                    page.OpenPage();

                    page.FillForm(
                        item.firstName,
                        item.lastName,
                        item.address,
                        item.city,
                        item.state,
                        item.zipCode,
                        item.phone
                    );

                    page.ClickUpdate();

                    bool result = page.IsUpdateSuccess();

                    string actual = result ? "Profile Updated" : "Update Fail";

                    string status = (result == item.expected) ? "PASS" : "FAIL";

                    string screenshot = ScreenshotHelper.Capture(driver);

                    ExcelHelper.WriteResult(row, actual, status, screenshot);
                }
                catch
                {
                    ExcelHelper.WriteResult(row, "ERROR", "FAIL", "");
                }

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