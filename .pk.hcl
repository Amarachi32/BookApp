packer {
  required_plugins {
    azure = {
      version = ">= 1.0.0"
      source  = "github.com/hashicorp/azure"
    }
  }
}

variable "clientid" {
  default="fe9f82c7-9a8c-425b-84a1-626d0b6a677e"
}

variable "clientsecret" {
  default="58527daa-47b9-469e-8e94-4e763e7c5400"
}

variable "subscriptionid" {
  default="2b69a9e3-da62-4760-8d19-c61e2802fad0"
}

variable "tenantid" {
  default="4a055335-9e1b-4990-9bee-11dc0d2768e5"
}

variable "resource_group" {
    default ="rg_images"
}
variable "image_name" {
    default ="linuxWeb"
}
variable "image_version" {
    default = "0.0.1"
}

//AZ
source "azure-arm" "azurevm" {
  client_id = var.clientid
  client_secret = var.clientsecret
  subscription_id = var.subscriptionid
  tenant_id = var.tenantid

  managed_image_name = "${var.image_name}-v${var.image_version}"
  managed_image_resource_group_name = var.resource_group

  os_type = "Linux"
  image_publisher = "Canonical"
  image_offer = "UbuntuServer"
  image_sku = "18.04-LTS"
  location = "West Europe"
  vm_size = "Standard_DS2_V2"


  azure_tags = {
    version = var.image_version
    role = "WebServer"
  }
}


build {
  sources = ["sources.azure-arm.azurevm"]


  provisioner "shell" {
    inline =  [
        "apt-get update",
        "apt-get -y install nginx"
      ]
    execute_command  = "chmod +x {{ .Path }}; {{ .Vars }} sudo -E sh '{{ .Path }}'"
  }


  provisioner "shell" {
    inline = [
      "sleep 30",
      "/usr/sbin/waagent -force -deprovision+user && export HISTSIZE=0 && sync"
    ]
    execute_command  = "chmod +x {{ .Path }}; {{ .Vars }} sudo -E sh '{{ .Path }}'"
    inline_shebang = "/bin/sh -x"
  }

}