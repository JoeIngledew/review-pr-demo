terraform {
    required_providers {
        azurerm = {
            source  = "hashicorp/azurerm"
            version = "~> 3.0.2"
        }
    }
    backend "azurerm" {
        resource_group_name  = var.rg_name
        storage_account_name = var.storage_account_name
        container_name       = var.storage_account_container
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

resource "azurerm_app_service_plan" "asp" {
    name = "asp-dev-jfi-pr-demo-${var.pr_number}"
    location = azurerm_resource_group.rg.location
    resource_group_name = azurerm_resource_group.rg.name
    kind = "Linux"
    reserved = true

    sku {
        tier = "Basic"
        size = "B1"
    }

    tags = {
        environment = "Dev/Test"
    }
}

resource "azurerm_app_service" "app" {
    name = "app-dev-jfi-pr-demo-${var.pr_number}"
    location = azurerm_resource_group.rg.location
    resource_group_name = azurerm_resource_group.rg.name
    app_service_plan_id = azurerm_app_service_plan.asp.id

    site_config {
      linux_fx_version = "DOTNETCORE|6.0"
    }

    app_settings = {}

    tags = {
        environment = "Dev/Test"
    }
}

output "app_service_url" {
    value = azurerm_app_service.app.default_site_hostname
    description = "Service URL for PR ${var.pr_number}"
}