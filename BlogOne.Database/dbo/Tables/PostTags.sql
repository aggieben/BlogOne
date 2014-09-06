CREATE TABLE [dbo].[PostTags]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Sid] UNIQUEIDENTIFIER NOT NULL DEFAULT newid(), 
    [PostId] INT NOT NULL, 
    [TagId] INT NOT NULL, 
    [CreationDate] DATETIME NOT NULL DEFAULT getutcdate(), 
    CONSTRAINT [FK_PostTags_ToTags] FOREIGN KEY ([TagId]) REFERENCES [Tags]([Id]), 
    CONSTRAINT [FK_PostTags_ToPosts] FOREIGN KEY ([PostId]) REFERENCES [Posts]([Id])
)
