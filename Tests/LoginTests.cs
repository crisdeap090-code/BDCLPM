
using OpenQA.Selenium;
using SeleniumParaBank.Pages;
using SeleniumParaBankTest.Models;

namespace SeleniumParaBankTest.Tests;

public class LoginTests : TestBase
{
    private readonly IWebDriver _driver;

    public LoginTests(IWebDriver driver, RunnerSettings settings) : base(driver, settings)
    {
        _driver = driver;
    }

    public ExecutionResult CheckLoginFormControls()
    {
        Resolver.GoHome();
        var page = new LoginPage(_driver);

        return page.AreLoginControlsVisible()
            ? ExecutionResult.Pass("Hiển thị Username, Password và nút Login")
            : ExecutionResult.Fail("Thiếu control trên form đăng nhập");
    }

    public ExecutionResult CheckInputWorks(LoginTestData data)
    {
        Resolver.GoHome();
        var page = new LoginPage(_driver);
        page.Login(data.Username, data.Password);

        bool successOrError = page.IsLoginSuccess() || page.HasLoginError();
        return successOrError
            ? ExecutionResult.Pass("Nhập được dữ liệu vào ô Username và Password")
            : ExecutionResult.Fail("Không xác nhận được việc nhập dữ liệu");
    }

    public ExecutionResult Run(LoginTestData data)
    {
        Resolver.GoHome();
        var page = new LoginPage(_driver);
        page.Login(data.Username, data.Password);

        bool success = page.IsLoginSuccess();

        if (ExpectPass(data.Expected))
        {
            return success
                ? ExecutionResult.Pass("Đăng nhập thành công")
                : ExecutionResult.Fail("Đăng nhập thất bại");
        }

        return !success
            ? ExecutionResult.Pass("Đăng nhập bị từ chối đúng mong đợi")
            : ExecutionResult.Fail("Đăng nhập thành công ngoài mong đợi");
    }
}
