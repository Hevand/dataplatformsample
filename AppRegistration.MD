# OData API App Registration

This is a description of the App Registration for the OData API (used by the PowerQuery Connector)

Goto Azure AD tenant

#### App Registration

-> New Registration

    Name: Name for your app (isv-data-platform)
    Supported account types: Accounts in any organizational directory (Any Azure AD - Multitenant)
    Redirect Url: Public client/native -> https://login.microsoftonline.com/common/oauth2/nativeclient
    
#### Once created

-> API Permissions 

    You can remove the Graph API
   
-> Expose an API

    Application ID URI: e.g. api://isv-data-platform
    -> Add a scope
        User.Read (used by PowerQuery Connector)
    -> Add a scope
        User.ReadWrite (not used by PowerQuery Connector, but for future scenario's like updating data)
