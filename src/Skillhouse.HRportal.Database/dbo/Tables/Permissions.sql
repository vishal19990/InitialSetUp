CREATE TABLE [dbo].[Permissions] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (50)  NOT NULL,
    [Description] VARCHAR (MAX) NOT NULL,
    [CreatedBy]   INT           NOT NULL,
    [CreatedOn]   DATETIME      NOT NULL,
    [UpdatedBy]   INT           NOT NULL,
    [UpdatedOn]   DATETIME      NOT NULL,
    [IsDeleted]   BIT           NOT NULL,
    CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED ([Id] ASC)
);

