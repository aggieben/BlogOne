CREATE TABLE [dbo].[AspNetUsers] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Sid] UNIQUEIDENTIFIER NOT NULL DEFAULT newsequentialid(),
    [UserId]            NVARCHAR (128) NOT NULL,
    [UserName]      NVARCHAR (MAX) NULL,
    [PasswordHash]  NVARCHAR (MAX) NULL,
    [SecurityStamp] NVARCHAR (MAX) NULL,
    [Discriminator] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([UserId] ASC)
);

