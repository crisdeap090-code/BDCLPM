
using OpenQA.Selenium;
using SeleniumParaBank.Pages;
using SeleniumParaBankTest.Models;

namespace SeleniumParaBankTest.Tests;

public class AccountOverviewTests : TestBase
{
    private readonly IWebDriver _driver;

    public AccountOverviewTests(IWebDriver driver, RunnerSettings settings) : base(driver, settings)
    {
        _driver = driver;
    }

    public ExecutionResult CheckAccountServicesMenu()
    {
        Resolver.LoginAsExistingUser();
        return new AccountOverviewPage(_driver).IsAccountServicesMenuDisplayed()
            ? ExecutionResult.Pass("Hiển thị menu Account Services")
            : ExecutionResult.Fail("Không thấy menu Account Services");
    }

    public ExecutionResult OpenOverviewPage()
    {
        Resolver.LoginAsExistingUser();
        var page = new AccountOverviewPage(_driver);
        page.OpenAccountOverview();

        return page.IsPageDisplayed()
            ? ExecutionResult.Pass("Mở trang Accounts Overview thành công")
            : ExecutionResult.Fail("Không mở được trang Accounts Overview");
    }

    public ExecutionResult CheckAccountListAndBalances()
    {
        Resolver.LoginAsExistingUser();
        var page = new AccountOverviewPage(_driver);
        page.OpenAccountOverview();

        bool ok = page.IsAccountTableDisplayed() && page.HasBalanceInformation();
        return ok
            ? ExecutionResult.Pass("Hiển thị danh sách tài khoản và số dư")
            : ExecutionResult.Fail("Thiếu bảng tài khoản hoặc thông tin số dư");
    }

    public ExecutionResult OpenAccountDetail()
    {
        Resolver.LoginAsExistingUser();
        var page = new AccountOverviewPage(_driver);
        page.OpenAccountOverview();
        page.ClickFirstAccount();

        return page.IsAccountDetailDisplayed()
            ? ExecutionResult.Pass("Mở trang chi tiết tài khoản thành công")
            : ExecutionResult.Fail("Không mở được chi tiết tài khoản");
    }

    public ExecutionResult CheckTransactionHistory()
    {
        Resolver.LoginAsExistingUser();
        var page = new AccountOverviewPage(_driver);
        page.OpenAccountOverview();
        page.ClickFirstAccount();

        return page.IsTransactionTableDisplayed()
            ? ExecutionResult.Pass("Hiển thị lịch sử giao dịch")
            : ExecutionResult.Fail("Không thấy lịch sử giao dịch");
    }

    public ExecutionResult CheckTransactionHeaders()
    {
        Resolver.LoginAsExistingUser();
        var page = new AccountOverviewPage(_driver);
        page.OpenAccountOverview();
        page.ClickFirstAccount();

        return page.HasTransactionHeaders()
            ? ExecutionResult.Pass("Hiển thị đủ cột ngày, loại giao dịch, số tiền")
            : ExecutionResult.Fail("Thiếu cột trong transaction list");
    }
}
