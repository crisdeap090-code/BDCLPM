
using OpenQA.Selenium;
using SeleniumParaBank.Pages;
using SeleniumParaBankTest.Models;

namespace SeleniumParaBankTest.Tests;

public class TransferFundsTests : TestBase
{
    private readonly IWebDriver _driver;

    public TransferFundsTests(IWebDriver driver, RunnerSettings settings) : base(driver, settings)
    {
        _driver = driver;
    }

    public ExecutionResult OpenPage()
    {
        Resolver.LoginAsExistingUser();
        var page = new TransferFundsPage(_driver);
        page.OpenPage();

        return page.IsPageDisplayed()
            ? ExecutionResult.Pass("Mở trang Transfer Funds thành công")
            : ExecutionResult.Fail("Không mở được trang Transfer Funds");
    }

    public ExecutionResult CheckTransferForm()
    {
        Resolver.LoginAsExistingUser();
        var page = new TransferFundsPage(_driver);
        page.OpenPage();

        return page.AreControlsDisplayed()
            ? ExecutionResult.Pass("Hiển thị đầy đủ form chuyển tiền")
            : ExecutionResult.Fail("Thiếu control trên form chuyển tiền");
    }

    public ExecutionResult Run(TransferTestData data)
    {
        Resolver.LoginAsExistingUser();
        var page = new TransferFundsPage(_driver);
        page.OpenPage();

        string from = Resolver.ResolveAccountAlias(data.FromAccount);
        string to = Resolver.ResolveAccountAlias(data.ToAccount);
        string amount = Resolver.NormalizeAmount(data.Amount);

        page.Transfer(amount, from, to);
        bool success = page.IsTransferSuccess();

        if (ExpectPass(data.Expected))
        {
            return success
                ? ExecutionResult.Pass("Chuyển tiền thành công")
                : ExecutionResult.Fail("Chuyển tiền thất bại");
        }

        return !success
            ? ExecutionResult.Pass("Chuyển tiền bị từ chối đúng mong đợi")
            : ExecutionResult.Fail("Chuyển tiền thành công ngoài mong đợi");
    }

    public ExecutionResult CheckConfirmation(TransferTestData data)
    {
        Resolver.LoginAsExistingUser();
        var page = new TransferFundsPage(_driver);
        page.OpenPage();

        page.Transfer(
            Resolver.NormalizeAmount(data.Amount),
            Resolver.ResolveAccountAlias(data.FromAccount),
            Resolver.ResolveAccountAlias(data.ToAccount)
        );

        return page.HasConfirmation()
            ? ExecutionResult.Pass("Hiển thị thông tin xác nhận sau khi chuyển tiền")
            : ExecutionResult.Fail("Không thấy thông tin xác nhận sau khi chuyển tiền");
    }

    public ExecutionResult CheckBalanceUpdated(TransferTestData data)
    {
        Resolver.LoginAsExistingUser();
        var overview = new AccountOverviewPage(_driver);
        overview.OpenAccountOverview();

        var before = _driver.PageSource;

        var transferPage = new TransferFundsPage(_driver);
        transferPage.OpenPage();
        transferPage.Transfer(
            Resolver.NormalizeAmount(data.Amount),
            Resolver.ResolveAccountAlias(data.FromAccount),
            Resolver.ResolveAccountAlias(data.ToAccount)
        );

        overview.OpenAccountOverview();
        var after = _driver.PageSource;

        return before != after
            ? ExecutionResult.Pass("Số dư thay đổi sau khi chuyển tiền")
            : ExecutionResult.Fail("Không quan sát được thay đổi số dư sau khi chuyển tiền");
    }
}
