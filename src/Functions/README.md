# Azure Commands

myLocation=northeurope

myResourceGroup=az204-functions-rg

myStorageAccount=az204strgfunctionsacc

mySourceQueueName=myqueue-items-source

myTargetQueueName=myqueue-items-target

az group create --name $myResourceGroup --location $myLocation

az storage account create --name $myStorageAccount  --resource-group $myResourceGroup

az storage queue create -n $mySourceQueueName --account-name $myStorageAccount

az storage queue create -n myTargetQueueName --account-name $myStorageAccount

az group delete --name $myResourceGroup --no-wait