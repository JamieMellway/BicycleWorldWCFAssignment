USE [BicycleWorld]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 03/12/2013 18:15:46 ******/
SET IDENTITY_INSERT [dbo].[Customers] ON
INSERT [dbo].[Customers] ([CustomerId], [FirstName], [LastName], [Username], [PasswordHash], [PasswordSalt], [IsAdmin]) VALUES (1, N'Fred', N'Flintstone', N'Fred', N'B0ljLQwiMsolxj3oEEwRcL89BZn0uKBq73iSz16W708=', N'Enrd4cU9wkJFy8M1YeMQAY75XrvKekd18k9v+08kmTQ=', 1)
INSERT [dbo].[Customers] ([CustomerId], [FirstName], [LastName], [Username], [PasswordHash], [PasswordSalt], [IsAdmin]) VALUES (2, N'Bert', N'Andernie', N'Bert', N'VJGrKHZUcpsNd/q9ULHW1Ok1xrAVQSO2WM7e0emNBeU=', N'mKw25FqSvwThjAxno4lKebju+ssf03mypL9UhtaK4g8=', 0)
SET IDENTITY_INSERT [dbo].[Customers] OFF
