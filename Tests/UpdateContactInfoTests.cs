
using OpenQA.Selenium;
using SeleniumParaBank.Pages;
using SeleniumParaBankTest.Models;

namespace SeleniumParaBankTest.Tests;

public class UpdateContactInfoTests : TestBase
{
    private readonly IWebDriver _driver;

    public UpdateContactInfoTests(IWebDriver driver, RunnerSettings settings) : base(driver, settings)
    {
        _driver = driver;
    }

    public ExecutionResult OpenPage()
    {
        Resolver.LoginAsExistingUser();
        var page = new UpdateProfilePage(_driver);
        page.OpenPage();

        return page.IsPageDisplayed()
            ? ExecutionResult.Pass("Mở trang Update Contact Info thành công")
            : ExecutionResult.Fail("Không mở được trang Update Contact Info");
    }

    public ExecutionResult CheckPrefilledInfo()
    {
        Resolver.LoginAsExistingUser();
        var page = new UpdateProfilePage(_driver);
        page.OpenPage();

        return page.HasPrefilledCurrentData()
            ? ExecutionResult.Pass("Form đã hiển thị sẵn thông tin hiện tại")
            : ExecutionResult.Fail("Form chưa hiển thị sẵn thông tin hiện tại");
    }

    public ExecutionResult Run(UpdateContactTestData data)
    {
        Resolver.LoginAsExistingUser();
        var page = new UpdateProfilePage(_driver);
        page.OpenPage();

        string firstName = page.GetValue("customer.firstName");
        string lastName = page.GetValue("customer.lastName");

        page.FillForm(firstName, lastName, data.Address, data.City, data.State, data.ZipCode, data.Phone);
        page.ClickUpdate();

        bool success = page.IsUpdateSuccess();

        if (ExpectPass(data.Expected))
        {
            return success
                ? ExecutionResult.Pass("Cập nhật thông tin liên hệ thành công")
                : ExecutionResult.Fail("Cập nhật thông tin thất bại");
        }

        return !success
            ? ExecutionResult.Pass("Cập nhật bị từ chối đúng mong đợi")
            : ExecutionResult.Fail("Cập nhật thành công ngoài mong đợi");
    }

    public ExecutionResult CheckInfoPersists(UpdateContactTestData data)
    {
        Resolver.LoginAsExistingUser();
        var page = new UpdateProfilePage(_driver);
        page.OpenPage();

        string firstName = page.GetValue("customer.firstName");
        string lastName = page.GetValue("customer.lastName");

        page.FillForm(firstName, lastName, data.Address, data.City, data.State, data.ZipCode, data.Phone);
        page.ClickUpdate();

        page.OpenPage();
        bool saved = page.GetValue("customer.address.street") == data.Address
            && page.GetValue("customer.phoneNumber") == data.Phone;

        return saved
            ? ExecutionResult.Pass("Thông tin mới vẫn được lưu sau khi tải lại trang")
            : ExecutionResult.Fail("Thông tin mới không được lưu sau khi tải lại");
    }
}
