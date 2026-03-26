
using OpenQA.Selenium;
using SeleniumParaBank.Pages;
using SeleniumParaBankTest.Models;

namespace SeleniumParaBankTest.Tests;

public class FindTransactionsTests : TestBase
{
    private readonly IWebDriver _driver;

    public FindTransactionsTests(IWebDriver driver, RunnerSettings settings) : base(driver, settings)
    {
        _driver = driver;
    }

    public ExecutionResult OpenPage()
    {
        Resolver.LoginAsExistingUser();
        var page = new FindTransactionPage(_driver);
        page.OpenPage();

        return page.IsPageDisplayed()
            ? ExecutionResult.Pass("Mở trang Find Transactions thành công")
            : ExecutionResult.Fail("Không mở được trang Find Transactions");
    }

    public ExecutionResult CheckSearchOptions()
    {
        Resolver.LoginAsExistingUser();
        var page = new FindTransactionPage(_driver);
        page.OpenPage();

        return page.AreSearchControlsDisplayed()
            ? ExecutionResult.Pass("Hiển thị các tùy chọn tìm giao dịch")
            : ExecutionResult.Fail("Thiếu control tìm giao dịch");
    }

    public ExecutionResult Run(FindTransactionsTestData data)
    {
        Resolver.LoginAsExistingUser();
        var page = new FindTransactionPage(_driver);
        page.OpenPage();
        page.SelectAccount(Resolver.ResolveAccountAlias(data.Account));

        if (!string.IsNullOrWhiteSpace(data.Date))
            page.FindByDate(data.Date);
        else if (!string.IsNullOrWhiteSpace(data.FromDate) || !string.IsNullOrWhiteSpace(data.ToDate))
            page.FindByDateRange(data.FromDate, data.ToDate);
        else
            page.FindByAmount(data.Amount);

        bool success = page.HasResultTable() && !page.HasNoResultMessage();

        if (ExpectPass(data.Expected))
        {
            return success
                ? ExecutionResult.Pass("Tìm giao dịch thành công")
                : ExecutionResult.Fail("Không tìm được giao dịch theo điều kiện");
        }

        return !success
            ? ExecutionResult.Pass("Không có kết quả đúng như mong đợi")
            : ExecutionResult.Fail("Có kết quả ngoài mong đợi");
    }
}
