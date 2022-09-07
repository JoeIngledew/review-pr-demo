@a11y
Feature: Basic accessibility

Scenario: Home page is accessible
    Given I am on the home page
    Then accessibility tests pass for "home"

Scenario: Privacy page is accessible
    Given I am on the home page
    When I navigate to the privacy page
    Then I am on the privacy page
    Then accessibility tests pass for "privacy"