Feature: Basic e2e

@e2e
Scenario: Navigation works
    Given I am on the home page
    When I navigate to the privacy page
    Then I am on the privacy page
    Then the privacy policy says "Use this page to detail your site's privacy policy."