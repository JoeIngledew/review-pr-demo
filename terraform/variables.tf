variable "rg_name" {
    description = "Base resource group name"
    type = string
    default = "JFI_DemoPrWeb"
}

variable "location" {
    description = "Resource location"
    type = string
    default = "ukwest"
}

variable "storage_account_name" {
    description = "Backend state storage account"
    type = string
    default = "jfitfstore01"
}

variable "storage_account_container" {
    description = "Backend state storage account container"
    type = string
    default = "tfstate"
}

variable "pr_number" {
    description = "PR number"
    type = string
}