CREATE TABLE [dbo].[Tenants]
(
	[tenant_id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [tenant_aadtentantid] NVARCHAR(50) NOT NULL,
    [tenant_name] NVARCHAR(50) NOT NULL, 
    [tenant_dbserver] NVARCHAR(50) NOT NULL, 
    [tenant_dbname] NVARCHAR(50) NOT NULL
)
