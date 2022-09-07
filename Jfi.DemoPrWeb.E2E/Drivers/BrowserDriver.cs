namespace Jfi.DemoPrWeb.E2E.Drivers;

using System;

using Microsoft.Extensions.Configuration;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

public class BrowserDriver : IDisposable
{
    private readonly Lazy<IWebDriver> _cwdLazy;
    private readonly IConfiguration _config;
    private bool disposedValue;

    public BrowserDriver(IConfiguration config)
    {
        _cwdLazy = new Lazy<IWebDriver>(CreateWebDriver);
        _config = config;
    }

    public IWebDriver Current => _cwdLazy.Value;

    private IWebDriver CreateWebDriver()
    {
        if (_config["E2E:Browser"] == "firefox")
        {
            var ffDriverSvc = FirefoxDriverService.CreateDefaultService();
            var opt = new FirefoxOptions
            {
                AcceptInsecureCertificates = true,
            };

            return new FirefoxDriver(ffDriverSvc, opt);
        }


        var chromeOptions = new ChromeOptions
        {
            AcceptInsecureCertificates = true,
        };

        chromeOptions.AddArgument("--start-maximized");
        chromeOptions.AddArgument("--incognito");

        bool hasCiConfig = bool.TryParse(_config["E2E:CI"], out bool isInCI);
        if (hasCiConfig && isInCI)
        {
            chromeOptions.AddArgument("--headless");
            chromeOptions.AddArgument("--no-sandbox");
            chromeOptions.AddArgument("--disable-gpu");
            chromeOptions.AddArgument("--disable-dev-shm-usage");
            chromeOptions.AddArgument("--disable-extensions");
        }

        return new ChromeDriver(
            ChromeDriverService.CreateDefaultService(),
            chromeOptions
        );
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                if (_cwdLazy.IsValueCreated)
                    Current.Quit();
            }


            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}