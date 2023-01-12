# Azure commands:
az account list-locations -o table

myLocation=northeurope

myNameSpaceName=az204svcbus$RANDOM

az group create --name az204-svcbus-rg --location $myLocation

az servicebus namespace create \
    --resource-group az204-svcbus-rg \
    --name $myNameSpaceName \
    --location $myLocation

az servicebus queue create --resource-group az204-svcbus-rg \
    --namespace-name $myNameSpaceName \
    --name az204-queue

az group delete --name az204-svcbus-rg --no-wait