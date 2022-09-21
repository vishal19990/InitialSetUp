CREATE TABLE [dbo].[UserLanguages] (
    [Id]         INT      IDENTITY (1, 1) NOT NULL,
    [UserId]     INT      NOT NULL,
    [LanguageId] INT      NOT NULL,
    [CreatedBy]  INT      NOT NULL,
    [CreatedOn]  DATETIME NOT NULL,
    [UpdatedBy]  INT      NOT NULL,
    [UpdatedOn]  DATETIME NOT NULL,
    [IsDeleted]  BIT      NOT NULL,
    CONSTRAINT [PK_UserLanguages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserLanguages_Languages] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id]),
    CONSTRAINT [FK_UserLanguages_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

