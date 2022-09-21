CREATE TABLE [dbo].[RolePermissions] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [RoleId]       INT      NOT NULL,
    [PermissionId] INT      NOT NULL,
    [Read]         BIT      NOT NULL,
    [Write]        BIT      NOT NULL,
    [Delete]       BIT      NOT NULL,
    [CreatedBy]    INT      NOT NULL,
    [CreatedOn]    DATETIME NOT NULL,
    [UpdatedBy]    INT      NOT NULL,
    [UpdatedOn]    DATETIME NOT NULL,
    [IsDeleted]    BIT      NOT NULL,
    CONSTRAINT [PK_RolePermission] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RolePermission_Permissions] FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Permissions] ([Id]),
    CONSTRAINT [FK_RolePermission_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id])
);

