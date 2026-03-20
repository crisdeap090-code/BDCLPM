using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumParaBank.Pages;
using SeleniumParaBank.Utilities;
using SeleniumParaBankTest.Utilities;
using SeleniumParaBankTest.TestData;
using System.Linq;

namespace SeleniumParaBankTest.Tests
{
    public class FindTransactionTests
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
        public void Run_FindTransaction_Tests_From_Json()
        {
            var data = JsonDataReader.GetFindTransactionData();

            int row = 7;

            foreach (var item in data.Take(10))
            {
                try
                {
                    FindTransactionPage page = new FindTransactionPage(driver);

                    page.OpenPage();

                    bool result = false;

                    switch (item.action)
                    {
                        case "search_by_id":
                            page.SelectAccount(item.account);
                            page.EnterTransactionId(item.transactionId);
                            page.ClickFind();
                            result = page.IsResultDisplayed();
                            break;

                        case "search_by_date":
                            page.SelectAccount(item.account);
                            page.EnterDate(item.date);
                            page.ClickFind();
                            result = page.IsResultDisplayed();
                            break;

                        case "search_by_date_range":
                            page.EnterDateRange(item.fromDate, item.toDate);
                            page.ClickFind();
                            result = page.IsResultDisplayed();
                            break;

                        case "search_by_amount":
                            page.EnterAmount(item.amount);
                            page.ClickFind();
                            result = page.IsResultDisplayed();
                            break;

                        case "empty_search":
                            page.ClickFind();
                            result = page.IsResultDisplayed();
                            break;
                    }

                    string actual = result ? "Find Transaction Success" : "Find Transaction Fail";

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