
using OpenQA.Selenium;
using SeleniumParaBankTest.Models;
using SeleniumParaBankTest.Utilities;

namespace SeleniumParaBankTest.Tests;

[TestFixture]
public class ExcelDrivenTestRunner
{
    private RunnerSettings _settings = null!;

    [SetUp]
    public void Setup()
    {
        var settingsPath = Path.Combine(PathHelper.JsonFolder, "runnerSettings.json");
        _settings = JsonDataReader.ReadList<RunnerSettings>(settingsPath).FirstOrDefault() ?? new RunnerSettings();
    }

    [Test]
    public void Run_All_TestCases_From_Excel()
    {
        var excel = new ExcelHelper(PathHelper.ExcelFile, _settings);
        var testCases = excel.ReadPendingTestCases();

        foreach (var testCase in testCases)
        {
            IWebDriver? driver = null;
            ExecutionResult result = ExecutionResult.Blocked("Chưa chạy");

            try
            {
                driver = DriverFactory.InitDriver(testCase.Browser);
                result = Dispatch(driver, testCase);

                if (string.IsNullOrWhiteSpace(result.ScreenshotPath) &&
                    (_settings.CapturePassScreenshots || !result.Status.Equals("Pass", StringComparison.OrdinalIgnoreCase)))
                {
                    result.ScreenshotPath = ScreenshotHelper.Capture(driver, testCase.TestCaseId, result.Status);
                }
            }
            catch (Exception ex)
            {
                result = ExecutionResult.Fail("Lỗi hệ thống khi chạy test case", ex.Message);
                if (driver != null)
                {
                    result.ScreenshotPath = ScreenshotHelper.Capture(driver, testCase.TestCaseId, "ERROR");
                }
            }
            finally
            {
                excel.WriteExecutionResult(testCase, result);
                try { driver?.Quit(); } catch { }
            }
        }
    }

    private ExecutionResult Dispatch(IWebDriver driver, TestCaseRow tc)
    {
        return tc.TestCaseId switch
        {
            "TC01" => new HomeUiTests(driver, _settings).CheckRegisterLink(),
            "TC02" => new RegisterTests(driver, _settings).OpenRegisterPage(),
            "TC03" => new RegisterTests(driver, _settings).CheckRegisterFormFields(),
            "TC04" or "TC05" or "TC06" or "TC07" => new RegisterTests(driver, _settings).Run(ReadRequired<RegisterTestData>("registerData.json", tc.DataSetId)),
            "TC08" => new LoginTests(driver, _settings).CheckLoginFormControls(),
            "TC09" => new LoginTests(driver, _settings).CheckInputWorks(ReadRequired<LoginTestData>("loginData.json", tc.DataSetId)),
            "TC10" or "TC11" or "TC12" or "TC13" => new LoginTests(driver, _settings).Run(ReadRequired<LoginTestData>("loginData.json", tc.DataSetId)),
            "TC14" => new LogoutSecurityTests(driver, _settings).LogoutSuccessfully(),
            "TC15" => new LogoutSecurityTests(driver, _settings).PreventInternalAccessAfterLogout(),
            "TC16" => new AccountOverviewTests(driver, _settings).CheckAccountServicesMenu(),
            "TC17" => new AccountOverviewTests(driver, _settings).OpenOverviewPage(),
            "TC18" => new AccountOverviewTests(driver, _settings).CheckAccountListAndBalances(),
            "TC19" => new AccountOverviewTests(driver, _settings).OpenAccountDetail(),
            "TC20" => new AccountOverviewTests(driver, _settings).CheckTransactionHistory(),
            "TC21" => new AccountOverviewTests(driver, _settings).CheckTransactionHeaders(),
            "TC22" => new OpenAccountTests(driver, _settings).OpenPage(),
            "TC23" or "TC24" => new OpenAccountTests(driver, _settings).Run(ReadRequired<OpenAccountTestData>("openAccountData.json", tc.DataSetId)),
            "TC25" => new OpenAccountTests(driver, _settings).CheckNewAccountInOverview(ReadRequired<OpenAccountTestData>("openAccountData.json", tc.DataSetId)),
            "TC26" => new OpenAccountTests(driver, _settings).OpenNewAccountDetail(ReadRequired<OpenAccountTestData>("openAccountData.json", tc.DataSetId)),
            "TC27" => new TransferFundsTests(driver, _settings).OpenPage(),
            "TC28" => new TransferFundsTests(driver, _settings).CheckTransferForm(),
            "TC29" or "TC30" or "TC31" or "TC32" or "TC33" or "TC34" => new TransferFundsTests(driver, _settings).Run(ReadRequired<TransferTestData>("transferData.json", tc.DataSetId)),
            "TC35" => new TransferFundsTests(driver, _settings).CheckConfirmation(ReadRequired<TransferTestData>("transferData.json", tc.DataSetId)),
            "TC36" => new TransferFundsTests(driver, _settings).CheckBalanceUpdated(ReadRequired<TransferTestData>("transferData.json", tc.DataSetId)),
            "TC37" => new BillPayTests(driver, _settings).OpenPage(),
            "TC38" => new BillPayTests(driver, _settings).CheckBillPayForm(),
            "TC39" or "TC40" or "TC41" or "TC42" or "TC43" => new BillPayTests(driver, _settings).Run(ReadRequired<BillPayTestData>("billPayData.json", tc.DataSetId)),
            "TC44" => new BillPayTests(driver, _settings).CheckConfirmation(ReadRequired<BillPayTestData>("billPayData.json", tc.DataSetId)),
            "TC45" => new FindTransactionsTests(driver, _settings).OpenPage(),
            "TC46" => new FindTransactionsTests(driver, _settings).CheckSearchOptions(),
            "TC47" or "TC48" or "TC49" or "TC50" or "TC51" => new FindTransactionsTests(driver, _settings).Run(ReadRequired<FindTransactionsTestData>("findTransactionsData.json", tc.DataSetId)),
            "TC52" => new UpdateContactInfoTests(driver, _settings).OpenPage(),
            "TC53" => new UpdateContactInfoTests(driver, _settings).CheckPrefilledInfo(),
            "TC54" or "TC55" => new UpdateContactInfoTests(driver, _settings).Run(ReadRequired<UpdateContactTestData>("updateContactData.json", tc.DataSetId)),
            "TC56" => new UpdateContactInfoTests(driver, _settings).CheckInfoPersists(ReadRequired<UpdateContactTestData>("updateContactData.json", tc.DataSetId)),
            "TC57" => new RequestLoanTests(driver, _settings).OpenPage(),
            "TC58" or "TC59" or "TC60" => new RequestLoanTests(driver, _settings).Run(ReadRequired<RequestLoanTestData>("requestLoanData.json", tc.DataSetId)),
            _ => ExecutionResult.Blocked("Chưa map test case", tc.TestCaseId)
        };
    }

    private static T ReadRequired<T>(string jsonFile, string dataSetId) where T : class
    {
        var path = PathHelper.GetJsonPath(jsonFile);
        var item = JsonDataReader.FindByDataSetId<T>(path, dataSetId);
        return item ?? throw new InvalidOperationException($"Không tìm thấy dataSetId {dataSetId} trong {jsonFile}");
    }
}
