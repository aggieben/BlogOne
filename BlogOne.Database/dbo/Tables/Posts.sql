CREATE TABLE [dbo].[Posts]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Sid] UNIQUEIDENTIFIER NOT NULL DEFAULT (newid()), 
    [CreationDate] DATETIME NOT NULL DEFAULT (getutcdate()), 
    [Title] NVARCHAR(256) NULL, 
    [Body] TEXT NOT NULL, 
    [Slug] NVARCHAR(50) NULL, 
    [SlugHash] AS (checksum([Slug])) PERSISTED NOT NULL, 
    [Draft] BIT NOT NULL DEFAULT ((1)), 
    [ModifiedDate] DATETIME NULL, 
    [Subtitle] NVARCHAR(256) NULL
)

GO

CREATE INDEX [IX_Posts_SlugHash] ON [dbo].[Posts] ([SlugHash])
