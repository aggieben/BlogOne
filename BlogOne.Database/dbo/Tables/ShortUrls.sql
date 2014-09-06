CREATE TABLE [dbo].[ShortUrls]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Sid] UNIQUEIDENTIFIER NOT NULL DEFAULT (newid()), 
    [CreationDate] DATETIME NOT NULL DEFAULT (getutcdate()), 
    [Enabled] BIT NOT NULL DEFAULT 1, 
    [Url] NVARCHAR(MAX) NOT NULL, 
    [ShortCode] NVARCHAR(10) NULL
)
