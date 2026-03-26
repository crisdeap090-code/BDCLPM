
# Selenium ParaBank Excel + JSON Runner

## Luồng chạy
- Đọc danh sách test case từ `TestData/Excel/Nhom3_AutoMation_Testing.xlsx`
- Case nào có `Data Sheet` và `Data Set ID` thì đọc JSON tương ứng trong `TestData/Json`
- Case nào không có data thì chạy trực tiếp
- Ghi kết quả lại vào sheet `TestExecution`
- Lưu screenshot vào `Reports/Screenshots/yyyy-MM-dd`

## File cần chỉnh trước khi chạy
1. `TestData/Json/runnerSettings.json`
   - `ExistingUsername`
   - `ExistingPassword`
   - `BaseUrl`

2. Nếu muốn chạy lại toàn bộ dù Excel đã có kết quả:
   - đổi `OnlyRunBlankOrNotRunRows` thành `false`

## File test data
- `loginData.json`
- `registerData.json`
- `openAccountData.json`
- `transferData.json`
- `billPayData.json`
- `findTransactionsData.json`
- `updateContactData.json`
- `requestLoanData.json`

## Entry point
- `Tests/ExcelDrivenTestRunner.cs`

## Ghi chú
- Một số case phụ thuộc dữ liệu thật trên ParaBank nên nếu tài khoản test không đúng hoặc dữ liệu hệ thống thay đổi, kết quả thực tế có thể khác.
- Với case đăng ký thành công, code tự thêm timestamp vào username để tránh bị trùng khi chạy nhiều lần.


## File chính là:

Tests/ExcelDrivenTestRunner.cs

Khi chạy test:

đọc cấu hình từ TestData/Json/runnerSettings.json
mở file Excel
lấy danh sách test case cần chạy
với từng test case:
tạo ChromeDriver
gọi Dispatch(...)
nhận ExecutionResult
chụp màn hình nếu cần
ghi kết quả về Excel
đóng browser

