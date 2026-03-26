
using OpenQA.Selenium;
using SeleniumParaBank.Pages;
using SeleniumParaBankTest.Models;

namespace SeleniumParaBankTest.Tests;

public class BillPayTests : TestBase
{
    private readonly IWebDriver _driver;

    public BillPayTests(IWebDriver driver, RunnerSettings settings) : base(driver, settings)
    {
        _driver = driver;
    }

    public ExecutionResult OpenPage()
    {
        Resolver.LoginAsExistingUser();
        var page = new BillPayPage(_driver);
        page.OpenBillPay();

        return page.IsPageDisplayed()
            ? ExecutionResult.Pass("Mở trang Bill Pay thành công")
            : ExecutionResult.Fail("Không mở được trang Bill Pay");
    }

    public ExecutionResult CheckBillPayForm()
    {
        Resolver.LoginAsExistingUser();
        var page = new BillPayPage(_driver);
        page.OpenBillPay();

        return page.AreAllFieldsDisplayed()
            ? ExecutionResult.Pass("Hiển thị đầy đủ các trường Bill Pay")
            : ExecutionResult.Fail("Thiếu trường trên form Bill Pay");
    }

    public ExecutionResult Run(BillPayTestData data)
    {
        Resolver.LoginAsExistingUser();
        var page = new BillPayPage(_driver);
        page.OpenBillPay();

        page.PayBill(
            data.PayeeName, data.Address, data.City, data.State,
            data.ZipCode, data.Phone, data.Account, data.VerifyAccount, PickFirstVariant(data.Amount)
        );

        bool success = page.IsPaymentSuccess();

        if (ExpectPass(data.Expected))
        {
            return success
                ? ExecutionResult.Pass("Thanh toán hóa đơn thành công")
                : ExecutionResult.Fail("Thanh toán hóa đơn thất bại");
        }

        return !success
            ? ExecutionResult.Pass("Thanh toán hóa đơn bị từ chối đúng mong đợi")
            : ExecutionResult.Fail("Thanh toán hóa đơn thành công ngoài mong đợi");
    }

    public ExecutionResult CheckConfirmation(BillPayTestData data)
    {
        Resolver.LoginAsExistingUser();
        var page = new BillPayPage(_driver);
        page.OpenBillPay();

        page.PayBill(
            data.PayeeName, data.Address, data.City, data.State,
            data.ZipCode, data.Phone, data.Account, data.VerifyAccount, PickFirstVariant(data.Amount)
        );

        return page.HasConfirmation()
            ? ExecutionResult.Pass("Hiển thị thông báo xác nhận sau khi thanh toán")
            : ExecutionResult.Fail("Không thấy thông báo xác nhận thanh toán");
    }
}
