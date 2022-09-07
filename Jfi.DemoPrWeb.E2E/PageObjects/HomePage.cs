namespace Jfi.DemoPrWeb.E2E.PageObjects;

using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;

public class HomePage : GenericPage
{
    public HomePage(IWebDriver driver, IConfiguration config) : base(driver, config)
    {
    }

    public IWebElement PrivacyNav => _driver.FindElement(By.LinkText("Privacy"));

    public override string PageUrl => _config["E2E:BaseUrl"] ?? "localhost";
}
