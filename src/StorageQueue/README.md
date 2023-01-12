# Azure Commands

myLocation=northeurope

myResourceGroup=az204-strgqueue-rg

myStorageAccount=az204strgqueueacc

az group create --name $myResourceGroup --location $myLocation

az storage account create --name $myStorageAccount  --resource-group $myResourceGroup

az group delete --name $myResourceGroup --no-wait