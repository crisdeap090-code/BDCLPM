using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumParaBank.Pages;
using SeleniumParaBank.Utilities;
using SeleniumParaBankTest.Utilities;
using SeleniumParaBankTest.TestData;
using System.Linq;

namespace SeleniumParaBankTest.Tests
{
    public class TransferFundsTests
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
        public void Run_TransferFunds_Tests_From_Json()
        {
            var data = JsonDataReader.GetTransferFundsData();

            int row = 7;

            foreach (var item in data.Take(10))
            {
                try
                {
                    TransferFundsPage page = new TransferFundsPage(driver);

                    page.OpenPage();

                    if (!string.IsNullOrEmpty(item.amount))
                        page.EnterAmount(item.amount);

                    if (!string.IsNullOrEmpty(item.fromAccount))
                        page.SelectFromAccount(item.fromAccount);

                    if (!string.IsNullOrEmpty(item.toAccount))
                        page.SelectToAccount(item.toAccount);

                    page.ClickTransfer();

                    bool result = page.IsTransferSuccess();

                    string actual = result ? "Transfer Success" : "Transfer Fail";

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