@description('Location where the databases are created')
param location string = resourceGroup().location

@description('Environment in which the resources are deployed')
@allowed([
  'dev'
  'test'
  'qa'
  'prod'
])
param environment string = 'prod'

@description('Name of the logical SQL Server instance')
param serverName string = 'sql-${resourceGroup().name}-${environment}-${location}'

@description('Name of the sql databases')
param sqlDBName string = 'Customer1'

resource server 'Microsoft.Sql/servers@2019-06-01-preview' existing= {
  name: serverName
}

resource sqlDB 'Microsoft.Sql/servers/databases@2020-08-01-preview' = {
  name: toLower('${server.name}/sql-${sqlDBName}-${environment}')
  location: location
  sku: {
    name: 'GP_S_Gen5'
    tier: 'GeneralPurpose'
    family: 'Gen5'
    capacity: 2
  }
  properties: {
    sampleName: 'AdventureWorksLT'
  }
}
