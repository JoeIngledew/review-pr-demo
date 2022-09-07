terraform {
    required_providers {
        azurerm = {
            source  = "hashicorp/azurerm"
            version = "~> 3.0.2"
        }
    }
    backend "azurerm" {
        resource_group_name  = "JFI_DemoPrWeb"
        storage_account_name = "jfitfstore01"
        container_name       = "tfstate"
        key                  = "terraform.tfstate"
    }

    required_version = ">= 1.1.0"
}

provider "azurerm" {
    features {}
}

resource "azurerm_resource_group" "rg" {
    name = var.rg_name
    location = var.location

    tags = {
        environment = "Dev/Test"
    }
}

resource "azurerm_service_plan" "asp" {
    name = "asp-dev-jfi-pr-demo-${var.pr_number}"
    location = azurerm_resource_group.rg.location
    resource_group_name = azurerm_resource_group.rg.name
    os_type = "Linux"
    reserved = true
    sku_name = "B1"

    tags = {
        environment = "Dev/Test"
    }
}

resource "azurerm_linux_web_app" "app" {
    name = "app-dev-jfi-pr-demo-${var.pr_number}"
    location = azurerm_resource_group.rg.location
    resource_group_name = azurerm_resource_group.rg.name
    service_plan_id = azurerm_service_plan.asp.id

    site_config {
      application_stack {
        dotnet_version = "6.0"
      }
    }

    app_settings = {}

    tags = {
        environment = "Dev/Test"
    }
}

output "app_service_url" {
    value = azurerm_linux_web_app.app.default_hostname
    description = "Service URL for PR"
}