
using OpenQA.Selenium;
using SeleniumParaBank.Pages;
using SeleniumParaBankTest.Models;

namespace SeleniumParaBankTest.Tests;

public class HomeUiTests : TestBase
{
    private readonly IWebDriver _driver;

    public HomeUiTests(IWebDriver driver, RunnerSettings settings) : base(driver, settings)
    {
        _driver = driver;
    }

    public ExecutionResult CheckRegisterLink()
    {
        Resolver.GoHome();
        var loginPage = new LoginPage(_driver);

        return loginPage.IsRegisterLinkVisible()
            ? ExecutionResult.Pass("Link Register hiển thị rõ ràng và có thể sử dụng")
            : ExecutionResult.Fail("Không thấy link Register trên trang chủ");
    }
}
