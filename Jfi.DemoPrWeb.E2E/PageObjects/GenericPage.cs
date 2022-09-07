namespace Jfi.DemoPrWeb.E2E.PageObjects;

using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;

public abstract class GenericPage
{
    protected readonly IWebDriver _driver;
    protected readonly IConfiguration _config;

    public GenericPage(IWebDriver driver, IConfiguration config)
    {
        _driver = driver;
        _config = config;
    }

    public abstract string PageUrl { get; }

    public void Navigate()
    {
        _driver.Navigate().GoToUrl(PageUrl);
    }
}