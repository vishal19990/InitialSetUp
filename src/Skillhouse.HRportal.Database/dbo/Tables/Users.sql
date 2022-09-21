CREATE TABLE [dbo].[Users] (
    [Id]                        INT            IDENTITY (1, 1) NOT NULL,
    [Name]                      VARCHAR (MAX)  NOT NULL,
    [ContactNumber]             VARCHAR (15)   NOT NULL,
    [Email]                     NVARCHAR (150) NOT NULL,
    [Password]                  VARCHAR (MAX)  NOT NULL,
    [Status]                    INT            NOT NULL,
    [UnsuccessfulLoginAttempts] INT            NULL,
    [LastLogin]                 DATETIME       NULL,
    [CreatedBy]                 INT            NOT NULL,
    [CreatedOn]                 DATETIME       NOT NULL,
    [UpdatedBy]                 INT            NOT NULL,
    [UpdatedOn]                 DATETIME       NOT NULL,
    [IsDeleted]                 BIT            NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);



