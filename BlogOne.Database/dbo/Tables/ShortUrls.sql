CREATE TABLE [dbo].[Table1]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Sid] UNIQUEIDENTIFIER NOT NULL DEFAULT (newid()), 
    [CreationDate] DATETIME NOT NULL DEFAULT (getutcdate()), 
    [Enabled] BIT NOT NULL DEFAULT 1, 
    [Url] NVARCHAR(MAX) NOT NULL, 
    [ShortCode] NVARCHAR(10) NOT NULL
)
