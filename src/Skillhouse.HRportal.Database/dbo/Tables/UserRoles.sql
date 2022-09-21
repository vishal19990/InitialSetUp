CREATE TABLE [dbo].[UserRoles] (
    [Id]        INT      IDENTITY (1, 1) NOT NULL,
    [UserId]    INT      NOT NULL,
    [RoleId]    INT      NOT NULL,
    [CreatedBy] INT      NOT NULL,
    [CreatedOn] DATETIME NOT NULL,
    [UpdatedBy] INT      NOT NULL,
    [UpdatedOn] DATETIME NOT NULL,
    [IsDeleted] BIT      NOT NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserRole_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]),
    CONSTRAINT [FK_UserRole_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

