
using OpenQA.Selenium;
using SeleniumParaBank.Pages;
using SeleniumParaBankTest.Models;

namespace SeleniumParaBankTest.Tests;

public class OpenAccountTests : TestBase
{
    private readonly IWebDriver _driver;

    public OpenAccountTests(IWebDriver driver, RunnerSettings settings) : base(driver, settings)
    {
        _driver = driver;
    }

    public ExecutionResult OpenPage()
    {
        Resolver.LoginAsExistingUser();
        var page = new OpenAccountPage(_driver);
        page.OpenPage();

        return page.IsPageDisplayed()
            ? ExecutionResult.Pass("Mở trang Open New Account thành công")
            : ExecutionResult.Fail("Không mở được trang Open New Account");
    }

    public ExecutionResult Run(OpenAccountTestData data)
    {
        Resolver.LoginAsExistingUser();
        var page = new OpenAccountPage(_driver);
        page.OpenPage();

        var fromAccount = Resolver.ResolveAccountAlias(data.FromAccount);
        page.OpenNewAccount(data.AccountType, fromAccount);

        bool success = page.IsAccountCreated();

        if (ExpectPass(data.Expected))
        {
            return success
                ? ExecutionResult.Pass($"Tạo tài khoản {data.AccountType} thành công")
                : ExecutionResult.Fail($"Không tạo được tài khoản {data.AccountType}");
        }

        return !success
            ? ExecutionResult.Pass("Tạo tài khoản bị từ chối đúng mong đợi")
            : ExecutionResult.Fail("Tạo tài khoản thành công ngoài mong đợi");
    }

    public ExecutionResult CheckNewAccountInOverview(OpenAccountTestData data)
    {
        Resolver.LoginAsExistingUser();
        var page = new OpenAccountPage(_driver);
        page.OpenPage();

        var fromAccount = Resolver.ResolveAccountAlias(data.FromAccount);
        page.OpenNewAccount(data.AccountType, fromAccount);

        var newId = page.GetNewAccountId();
        if (string.IsNullOrWhiteSpace(newId))
            return ExecutionResult.Fail("Không lấy được mã tài khoản mới");

        var overview = new AccountOverviewPage(_driver);
        overview.OpenAccountOverview();
        bool exists = overview.GetAccountNumbers().Contains(newId);

        return exists
            ? ExecutionResult.Pass("Tài khoản mới xuất hiện trong Accounts Overview")
            : ExecutionResult.Fail("Không thấy tài khoản mới trong Accounts Overview");
    }

    public ExecutionResult OpenNewAccountDetail(OpenAccountTestData data)
    {
        Resolver.LoginAsExistingUser();
        var page = new OpenAccountPage(_driver);
        page.OpenPage();

        var fromAccount = Resolver.ResolveAccountAlias(data.FromAccount);
        page.OpenNewAccount(data.AccountType, fromAccount);

        var newId = page.GetNewAccountId();
        if (string.IsNullOrWhiteSpace(newId))
            return ExecutionResult.Fail("Không lấy được mã tài khoản mới");

        _driver.FindElement(By.LinkText(newId)).Click();

        return _driver.PageSource.Contains("Account Details")
            ? ExecutionResult.Pass("Mở được chi tiết tài khoản vừa tạo")
            : ExecutionResult.Fail("Không mở được chi tiết tài khoản vừa tạo");
    }
}
