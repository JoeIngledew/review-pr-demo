namespace Jfi.DemoPrWeb.E2E.PageObjects;

using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;

public class PrivacyPage : GenericPage
{
    public PrivacyPage(IWebDriver driver, IConfiguration config) : base(driver, config)
    {
    }

    public IWebElement BodyText => _driver.FindElement(By.Id("privacy-text"));

    public override string PageUrl => $"{_config["E2E:BaseUrl"] ?? "localhost"}/Privacy";
}
