namespace Jfi.DemoPrWeb.E2E.Hooks;

using BoDi;
using Microsoft.Extensions.Configuration;

using TechTalk.SpecFlow;

[Binding]
public class ConfigurationHook
{
    [BeforeTestRun]
    public static void BeforeTestRun(ObjectContainer testThreadContainer)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("testsettings.json", false, false)
            .AddJsonFile("testsettings.Development.json", true, false)
            .AddEnvironmentVariables()
            .Build();

        testThreadContainer.BaseContainer.RegisterInstanceAs(config);
    }
}
