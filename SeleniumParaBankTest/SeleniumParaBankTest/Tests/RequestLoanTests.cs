using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumParaBank.Pages;
using SeleniumParaBank.Utilities;
using SeleniumParaBankTest.Utilities;
using SeleniumParaBankTest.TestData;
using System.Linq;

namespace SeleniumParaBankTest.Tests
{
    public class RequestLoanTests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = DriverFactory.InitDriver();

            driver.Navigate().GoToUrl("https://parabank.parasoft.com/parabank/index.htm");

            LoginPage login = new LoginPage(driver);
            login.Login("ThizKi3u", "19032022");
        }

        [Test]
        public void Run_RequestLoan_Tests_From_Json()
        {
            var data = JsonDataReader.GetRequestLoanData();

            int row = 7;

            foreach (var item in data.Take(10))
            {
                try
                {
                    RequestLoanPage page = new RequestLoanPage(driver);

                    page.OpenPage();

                    page.EnterLoanAmount(item.loanAmount);

                    page.EnterDownPayment(item.downPayment);

                    page.ClickApply();

                    bool result = page.IsLoanApproved();

                    string actual = result ? "Loan Approved" : "Loan Fail";

                    string status = (result == item.expected) ? "PASS" : "FAIL";

                    string screenshot = ScreenshotHelper.Capture(driver);

                    ExcelHelper.WriteResult(row, actual, status, screenshot);
                }
                catch
                {
                    ExcelHelper.WriteResult(row, "ERROR", "FAIL", "");
                }

                row += 2;
            }
        }

        [TearDown]
        public void Close()
        {
            driver.Quit();
        }
    }
}