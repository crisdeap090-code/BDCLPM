using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumParaBank.Pages;
using SeleniumParaBank.Utilities;
using SeleniumParaBankTest.Utilities;
using SeleniumParaBankTest.TestData;
using System.Linq;

namespace SeleniumParaBankTest.Tests
{
    public class BillPayTests
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
        public void Run_BillPay_Tests_From_Json()
        {
            var data = JsonDataReader.GetBillPayData();

            int row = 7;

            foreach (var item in data.Take(10))
            {
                try
                {
                    BillPayPage page = new BillPayPage(driver);

                    //  mở trang Bill Pay (đã sửa đúng tên hàm)
                    page.OpenBillPay();

                    //  nhập dữ liệu (gọi từng hàm đúng)
                    page.EnterPayeeName(item.payeeName);
                    page.EnterAddress(item.address);
                    page.EnterCity(item.city);
                    page.EnterState(item.state);
                    page.EnterZip(item.zipCode);
                    page.EnterPhone(item.phone);
                    page.EnterAccount(item.account);
                    page.EnterVerifyAccount(item.verifyAccount);
                    page.EnterAmount(item.amount);

                    // click send payment
                    page.ClickSendPayment();

                    // kiểm tra kết quả
                    bool result = page.IsPaymentSuccess();

                    string actual = result ? "Payment Success" : "Payment Fail";
                    string status = (result == item.expected) ? "PASS" : "FAIL";

                    // chụp màn hình
                    string screenshot = ScreenshotHelper.Capture(driver);

                    // ghi Excel
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