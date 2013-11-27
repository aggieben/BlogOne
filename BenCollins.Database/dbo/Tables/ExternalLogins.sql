CREATE TABLE [dbo].[ExternalLogins]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Sid] UNIQUEIDENTIFIER NOT NULL DEFAULT newsequentialid(), 
    [DefaultUserName] NVARCHAR(128) NOT NULL, 
    [ProviderName] NVARCHAR(128) NOT NULL, 
    [ProviderKey] NVARCHAR(1024) NOT NULL
)
