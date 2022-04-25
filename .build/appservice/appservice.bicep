param location string = resourceGroup().location
param sku string = 'P1v2'
param linuxFxVersion = 'DOTNETCORE|6.0'
param appServicePlanName string = toLower('${resourceGroup().name}-asp-${location}')
param webAppName string = toLower('${resourceGroup().name}-app-${location}')

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
