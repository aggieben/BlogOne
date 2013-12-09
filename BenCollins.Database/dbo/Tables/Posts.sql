CREATE TABLE [dbo].[Posts]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Sid] UNIQUEIDENTIFIER NOT NULL DEFAULT (newid()), 
    [CreationDate] DATETIME NOT NULL DEFAULT (getutcdate()), 
    [Title] NVARCHAR(256) NOT NULL, 
    [Body] TEXT NOT NULL, 
    [Slug] NVARCHAR(50) NOT NULL, 
    [SlugHash] AS (checksum(Slug)) PERSISTED NOT NULL
)

GO

CREATE INDEX [IX_Posts_SlugHash] ON [dbo].[Posts] ([SlugHash])
