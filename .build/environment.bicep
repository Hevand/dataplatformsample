@description('Location of the resources')
param location string = resourceGroup().location

@description('SQL Server admin username')
param sqlAdminUser string = uniqueString(resourceGroup().id)

@description('SQL Server admin user password')
@secure()
param sqlAdminPwd string = newGuid()

@description('API management publisher email')
param publisheremail string = 'datamesh@sampleemail.com'

@description('API management publisher name')
param publishername string = resourceGroup().name

module appService './appservice/appservice.bicep' = {
  name: 'AppService-Deployment'
  params: {
    location: location
  }
}

module apimanagement './apimanagement/apimanagement.bicep' = {
  name: 'API-Management-Deployment'
  params: {
    publisherEmail: publisheremail
    publisherName: publishername
    location: location    
  }
}

module sqlServer './sql/server.bicep' = {
  name: 'SQL-Server-Deployment'
  params: {    
    administratorLogin: sqlAdminUser
    administratorLoginPassword: sqlAdminPwd
    location: location
  }
}

module sqlDb1 './sql/database.bicep' = {
  name: 'SQL-Db-Customer1-Deployment'
  dependsOn: [ 
    sqlServer 
  ]
  params: {
    sqlDBName: 'Customer1'    
    location: location
  }
}

module sqlDb2 './sql/database.bicep' = {
  name: 'SQL-Db-Customer2-Deployment'
  dependsOn: [ 
    sqlServer 
  ]
  params: {
    sqlDBName: 'Customer2'    
    location: location
  }
}
