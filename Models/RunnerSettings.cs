
namespace SeleniumParaBankTest.Models;

public class RunnerSettings
{
    public string BaseUrl { get; set; } = "https://parabank.parasoft.com/parabank/index.htm";
    public string ExistingUsername { get; set; } = "customer";
    public string ExistingPassword { get; set; } = "123";
    public bool CapturePassScreenshots { get; set; } = true;
    public bool OnlyRunBlankOrNotRunRows { get; set; } = true;
}
