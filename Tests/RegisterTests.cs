
using OpenQA.Selenium;
using SeleniumParaBank.Pages;
using SeleniumParaBankTest.Models;

namespace SeleniumParaBankTest.Tests;

public class RegisterTests : TestBase
{
    private readonly IWebDriver _driver;

    public RegisterTests(IWebDriver driver, RunnerSettings settings) : base(driver, settings)
    {
        _driver = driver;
    }

    public ExecutionResult OpenRegisterPage()
    {
        Resolver.GoHome();
        var loginPage = new LoginPage(_driver);
        loginPage.OpenRegisterPage();

        return new RegisterPage(_driver).IsPageDisplayed()
            ? ExecutionResult.Pass("Mở trang Register thành công")
            : ExecutionResult.Fail("Không mở được trang Register");
    }

    public ExecutionResult CheckRegisterFormFields()
    {
        Resolver.GoHome();
        new LoginPage(_driver).OpenRegisterPage();

        return new RegisterPage(_driver).AreAllFieldsDisplayed()
            ? ExecutionResult.Pass("Form đăng ký hiển thị đủ trường")
            : ExecutionResult.Fail("Thiếu một hoặc nhiều trường trên form đăng ký");
    }

    public ExecutionResult Run(RegisterTestData data)
    {
        Resolver.GoHome();
        new LoginPage(_driver).OpenRegisterPage();

        var page = new RegisterPage(_driver);
        var username = ExpectPass(data.Expected)
            ? Resolver.BuildUniqueUsername(data.Username)
            : data.Username;

        page.Register(
            data.FirstName, data.LastName, data.Address, data.City, data.State,
            data.ZipCode, data.Phone, data.SSN, username, data.Password, data.ConfirmPassword);

        if (page.IsRegisterSuccess())
        {
            return ExpectPass(data.Expected)
                ? ExecutionResult.Pass("Đăng ký tài khoản thành công")
                : ExecutionResult.Fail("Đăng ký thành công ngoài mong đợi");
        }

        if (page.HasRequiredValidationError())
        {
            return ExpectFail(data.Expected)
                ? ExecutionResult.Pass("Hiển thị lỗi bắt buộc đúng mong đợi")
                : ExecutionResult.Fail("Đăng ký thất bại do thiếu trường bắt buộc");
        }

        if (page.HasPasswordMismatchError())
        {
            return ExpectFail(data.Expected)
                ? ExecutionResult.Pass("Hiển thị lỗi mật khẩu không khớp")
                : ExecutionResult.Fail("Đăng ký thất bại do mật khẩu không khớp");
        }

        if (page.HasDuplicateUsernameError())
        {
            return ExpectFail(data.Expected)
                ? ExecutionResult.Pass("Hiển thị lỗi username đã tồn tại")
                : ExecutionResult.Fail("Đăng ký thất bại do username trùng");
        }

        return ExpectFail(data.Expected)
            ? ExecutionResult.Pass("Đăng ký bị từ chối đúng mong đợi")
            : ExecutionResult.Fail("Không thấy thông báo đăng ký thành công");
    }
}
