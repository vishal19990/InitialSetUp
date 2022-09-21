GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([Id], [Name], [ContactNumber], [Email], [Password], [Status], [UnsuccessfulLoginAttempts], [LastLogin], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn], [IsDeleted]) VALUES (1, N'HR', N'124', N'hr@gmail.com', N'57Ar/VCgtQqHjwl7aZM4KBfFSNwngPGIT3lcENSH7cA=', 1, 0, CAST(N'2022-03-08T05:11:53.520' AS DateTime), -1, CAST(N'2022-03-08T04:10:42.130' AS DateTime), -1, CAST(N'2022-03-08T05:11:53.520' AS DateTime), 0)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
