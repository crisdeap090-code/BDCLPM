
using OpenQA.Selenium;
using SeleniumParaBank.Pages;
using SeleniumParaBankTest.Models;

namespace SeleniumParaBankTest.Tests;

public class RequestLoanTests : TestBase
{
    private readonly IWebDriver _driver;

    public RequestLoanTests(IWebDriver driver, RunnerSettings settings) : base(driver, settings)
    {
        _driver = driver;
    }

    public ExecutionResult OpenPage()
    {
        Resolver.LoginAsExistingUser();
        var page = new RequestLoanPage(_driver);
        page.OpenPage();

        return page.IsPageDisplayed()
            ? ExecutionResult.Pass("Mở trang Request Loan thành công")
            : ExecutionResult.Fail("Không mở được trang Request Loan");
    }

    public ExecutionResult Run(RequestLoanTestData data)
    {
        Resolver.LoginAsExistingUser();
        var page = new RequestLoanPage(_driver);
        page.OpenPage();

        page.RequestLoan(
            PickFirstVariant(data.LoanAmount),
            PickFirstVariant(data.DownPayment),
            Resolver.ResolveAccountAlias(data.FromAccount)
        );

        bool success = page.IsLoanProcessed() && page.IsLoanApproved();

        if (ExpectPass(data.Expected))
        {
            return success
                ? ExecutionResult.Pass("Gửi yêu cầu vay thành công")
                : ExecutionResult.Fail("Gửi yêu cầu vay thất bại");
        }

        return !success
            ? ExecutionResult.Pass("Yêu cầu vay bị từ chối đúng mong đợi")
            : ExecutionResult.Fail("Yêu cầu vay thành công ngoài mong đợi");
    }
}
