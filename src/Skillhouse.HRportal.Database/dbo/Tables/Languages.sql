CREATE TABLE [dbo].[Languages] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Name]      VARCHAR (100) NOT NULL,
    [CreatedBy] INT           NOT NULL,
    [CreatedOn] DATETIME      NOT NULL,
    [UpdatedBy] INT           NOT NULL,
    [UpdatedOn] DATETIME      NOT NULL,
    [IsDeleted] BIT           NOT NULL,
    CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED ([Id] ASC)
);

