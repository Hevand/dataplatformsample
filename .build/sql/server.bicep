@description('Location where the databases are created')
param location string = resourceGroup().location

@description('Name of the logical SQL Server instance')
param serverName string = '${resourceGroup().name}-dbs-${location}'

@description('Admin username for SQL Server')
param administratorLogin string

@secure()
@description('Admin password for SQL Server')
param administratorLoginPassword string

resource server 'Microsoft.Sql/servers@2019-06-01-preview' = {
  name: serverName
  location: location
  properties: {
    administratorLogin: administratorLogin
    administratorLoginPassword: administratorLoginPassword
  }
}
