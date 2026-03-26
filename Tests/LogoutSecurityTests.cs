
using OpenQA.Selenium;
using SeleniumParaBank.Pages;
using SeleniumParaBankTest.Models;

namespace SeleniumParaBankTest.Tests;

public class LogoutSecurityTests : TestBase
{
    private readonly IWebDriver _driver;

    public LogoutSecurityTests(IWebDriver driver, RunnerSettings settings) : base(driver, settings)
    {
        _driver = driver;
    }

    public ExecutionResult LogoutSuccessfully()
    {
        Resolver.LoginAsExistingUser();
        var logout = new LogoutPage(_driver);
        logout.ClickLogout();

        return logout.IsLoggedOut()
            ? ExecutionResult.Pass("Đăng xuất thành công")
            : ExecutionResult.Fail("Đăng xuất không thành công");
    }

    public ExecutionResult PreventInternalAccessAfterLogout()
    {
        Resolver.LoginAsExistingUser();
        new LogoutPage(_driver).ClickLogout();

        var overview = new AccountOverviewPage(_driver);
        bool canAccess = overview.CanAccessInternalPageAfterLogout();

        return !canAccess
            ? ExecutionResult.Pass("Không truy cập được trang nội bộ sau đăng xuất")
            : ExecutionResult.Fail("Vẫn truy cập được trang nội bộ sau đăng xuất");
    }
}
