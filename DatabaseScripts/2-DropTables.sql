USE [BicycleWorld]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SalesOrderSalesOrderItem]') AND parent_object_id = OBJECT_ID(N'[dbo].[SalesOrderItems]'))
ALTER TABLE [dbo].[SalesOrderItems] DROP CONSTRAINT [FK_SalesOrderSalesOrderItem]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TransactionHistory_Product_ProductID]') AND parent_object_id = OBJECT_ID(N'[dbo].[SalesOrderItems]'))
ALTER TABLE [dbo].[SalesOrderItems] DROP CONSTRAINT [FK_TransactionHistory_Product_ProductID]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrderItems]') AND type in (N'U'))
DROP TABLE [dbo].[SalesOrderItems]
GO


IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SalesOrderCustomer]') AND parent_object_id = OBJECT_ID(N'[dbo].[SalesOrders]'))
ALTER TABLE [dbo].[SalesOrders] DROP CONSTRAINT [FK_SalesOrderCustomer]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SalesOrders]') AND type in (N'U'))
DROP TABLE [dbo].[SalesOrders]
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customers]') AND type in (N'U'))
DROP TABLE [dbo].[Customers]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Product_ProductCategory_ProductCategoryID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Products]'))
ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_Product_ProductCategory_ProductCategoryID]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 03/12/2013 16:40:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND type in (N'U'))
DROP TABLE [dbo].[Products]
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductCategories]') AND type in (N'U'))
DROP TABLE [dbo].[ProductCategories]
GO
