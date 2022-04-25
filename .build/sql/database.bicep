@description('Location where the databases are created')
param location string = resourceGroup().location

@description('Name of the logical SQL Server instance')
param serverName string = '${resourceGroup().name}-dbs-${location}'

@description('Name of the sql databases')
param sqlDBName string = 'Sample DB1'

resource server 'Microsoft.Sql/servers@2019-06-01-preview' existing= {
  name: serverName
}

resource sqlDB 'Microsoft.Sql/servers/databases@2020-08-01-preview' = {
  name: '${server.name}/${sqlDBName}'
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
