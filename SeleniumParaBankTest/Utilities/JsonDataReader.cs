using Newtonsoft.Json;
using SeleniumParaBankTest.TestData;

namespace SeleniumParaBankTest.Utilities
{
    public class JsonDataReader
    {
        // Đọc dữ liệu Login
        public static List<UserData> GetUsers()
        {
            string path = "TestData/users.json";

            string json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<List<UserData>>(json) ?? new List<UserData>();
        }

        // Đọc dữ liệu Register
        public static List<RegisterData> GetRegisterData()
        {
            string path = "TestData/registerUsers.json";

            string json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<List<RegisterData>>(json)
                   ?? new List<RegisterData>();
        }

        // Đọc dữ liệu Transfer
        public static List<TransferFundsData> GetTransferFundsData()
        {
            string path = "TestData/transferFunds.json";

            string json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<List<TransferFundsData>>(json) ?? new List<TransferFundsData>();
        }

        // Đọc dữ liệu Logout
        public static List<LogoutData> GetLogoutData()
        {
            string path = "TestData/logoutUsers.json";

            string json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<List<LogoutData>>(json)
                   ?? new List<LogoutData>();
        }

        // Đọc dữ liệu Find Transaction
        public static List<FindTransactionData> GetFindTransactionData()
        {
            string path = "TestData/findTransactionUsers.json";

            string json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<List<FindTransactionData>>(json) ?? new List<FindTransactionData>();
        }

        // Đọc dữ liệu AccoutOverview
        public static List<AccountOverviewData> GetAccountOverviewData()
        {
            string path = "TestData/accountOverviewUsers.json";

            string json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<List<AccountOverviewData>>(json)
                   ?? new List<AccountOverviewData>();
        }

        // Đọc dữ liệu OpenAccount
        public static List<OpenAccountData> GetOpenAccountData()
        {
            string path = "TestData/openAccountUsers.json";

            string json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<List<OpenAccountData>>(json)
                   ?? new List<OpenAccountData>();
        }

        // Đọc dữ liệu BillPay
        public static List<BillPayData> GetBillPayData()
        {
            string path = "TestData/billPayUsers.json";

            string json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<List<BillPayData>>(json)
                   ?? new List<BillPayData>();
        }

        // Đọc dữ liệu RequestLoan
        public static List<RequestLoanData> GetRequestLoanData()
        {
            string path = "TestData/requestLoanUsers.json";

            string json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<List<RequestLoanData>>(json)
                   ?? new List<RequestLoanData>();
        }

        // Đọc dữ liệu Update
        public static List<UpdateProfileData> GetUpdateProfileData()
        {
            string path = "TestData/updateProfileUsers.json";

            string json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<List<UpdateProfileData>>(json)
                   ?? new List<UpdateProfileData>();
        }
    }
}