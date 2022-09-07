namespace Jfi.DemoPrWeb.E2E.Steps;

using System;
using System.IO;

using FluentAssertions;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using Selenium.Axe;
using TechTalk.SpecFlow;

using Drivers;
using PageObjects;

[Binding]
public class SharedSteps
{
    private readonly IWebDriver _driver;
    private readonly HomePage _homePage;
    private readonly PrivacyPage _privacyPage;

    public SharedSteps(BrowserDriver driver, IConfiguration config)
    {
        _driver = driver.Current;
        _homePage = new HomePage(_driver, config);
        _privacyPage = new PrivacyPage(_driver, config);
    }

    [Then(@"accessibility tests pass for ""([^""]*)""")]
    public void ThenAllAccessibilityChecksPassFor(string pageName)
    {
        var results = new AxeBuilder(_driver)
            .DisableRules("aria-allowed-attr")
            .Analyze();

        string path = Path.Combine(Environment.CurrentDirectory, $"A11Y_{pageName}.html");
        _driver.CreateAxeHtmlReport(results, path);

        results.Violations.Should().BeEmpty();
    }

    [Given(@"I am on the home page")]
    public void GivenIAmOnTheStartPage()
    {
        _homePage.Navigate();
    }

    [When(@"I navigate to the privacy page")]
    public void WhenINavigateToThePrivacyPage()
    {
        _homePage.PrivacyNav.Click();
    }

    [Then(@"I am on the privacy page")]
    public void ThenIAmOnThePrivacyPage()
    {
        _driver.Url.ToLower().Should().Be(_privacyPage.PageUrl.ToLower());
    }

    [Then(@"the privacy policy says ""([^""]*)""")]
    public void ThenThePrivacyPolicySays(string text)
    {
        _privacyPage.BodyText.Text.Should().Contain(text);
    }
}
