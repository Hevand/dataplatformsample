param publisheremail string = 'datamesh@sampleemail.com'
param publishername string = resourceGroup().name
param location string = resourceGroup().location

module appService './appservice/appservice.bicep' = {
  name: 'AppService Deployment'
  params: {
    location: location
  }
}

module apimanagement './apimanagement/apimanagement.bicep' = {
  name: 'API Management Deployment'
  params: {
    publisherEmail: publisheremail
    publisherName: publishername
    location: location
    sku: 'Developer'
    skuCount: 1
  }
}
