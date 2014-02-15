CREATE TABLE [dbo].[ExternalLogins]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Sid] UNIQUEIDENTIFIER NOT NULL DEFAULT (newid()), 
    [DefaultUserName] NVARCHAR(128) NOT NULL, 
    [LoginProvider] NVARCHAR(128) NOT NULL, 
    [ProviderKey] NVARCHAR(1024) NOT NULL, 
    [CreationDate] DATETIME NOT NULL DEFAULT (getutcdate()), 
    [ModifiedDate] DATETIME NULL
)
