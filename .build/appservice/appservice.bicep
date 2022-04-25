@description('Location for all resources.')
param location string = resourceGroup().location

@description('Environment in which the resources are deployed')
@allowed([
  'dev'
  'test'
  'qa'
  'prod'
])
param environment string = 'prod'

@description('The name of the app service plan')
param appServicePlanName string = toLower('asp-${resourceGroup().name}-${environment}-${location}-001')

@description('The name of the web app')
param webAppName string = toLower('app-${resourceGroup().name}-${environment}-001')

@description('The SKU of the app service plan')
@allowed([
  'S1'
  'S2'
  'S3'
  'P1v2'
  'P2v2'
  'P3v2'
  'P1v3'
  'P2v3'
  'P3v3'
])
param sku string = 'P1v2'


@description('The Linux FX version')
@allowed([
  'DOTNETCORE|3.1'
  'DOTNETCORE|5.0'
  'DOTNETCORE|6.0'
])
param linuxFxVersion string = 'DOTNETCORE|6.0'

resource appServicePlan 'Microsoft.Web/serverfarms@2021-03-01' = {
  name: appServicePlanName
  location: location
  properties: {
    reserved: true
  }
  sku: {
    name: sku
  }
}

resource webApp 'Microsoft.Web/sites@2021-03-01' = {
  name: webAppName
  location: location
  kind: 'app'
  properties: {    
    serverFarmId: appServicePlan.id
    siteConfig: {
      linuxFxVersion: linuxFxVersion
    }
  }
}
