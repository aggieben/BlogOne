CREATE TABLE [dbo].[ExternalLogins]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Sid] UNIQUEIDENTIFIER NULL DEFAULT newsequentialid(), 
    [DefaultUserName] NVARCHAR(128) NOT NULL, 
    [LoginProvider] NVARCHAR(128) NOT NULL, 
    [ProviderKey] NVARCHAR(1024) NOT NULL
)
