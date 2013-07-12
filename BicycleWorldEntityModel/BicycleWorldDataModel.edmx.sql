
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 04/02/2013 10:24:13
-- Generated from EDMX file: D:\CXEC304 JDM\Assignment\BicycleWorldEntityModel\BicycleWorldDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BicycleWorld];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Product_ProductCategory_ProductCategoryID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_Product_ProductCategory_ProductCategoryID];
GO
IF OBJECT_ID(N'[dbo].[FK_TransactionHistory_Product_ProductID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SalesOrderItems] DROP CONSTRAINT [FK_TransactionHistory_Product_ProductID];
GO
IF OBJECT_ID(N'[dbo].[FK_SalesOrderCustomer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SalesOrders] DROP CONSTRAINT [FK_SalesOrderCustomer];
GO
IF OBJECT_ID(N'[dbo].[FK_SalesOrderSalesOrderItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SalesOrderItems] DROP CONSTRAINT [FK_SalesOrderSalesOrderItem];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[ProductCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductCategories];
GO
IF OBJECT_ID(N'[dbo].[SalesOrderItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SalesOrderItems];
GO
IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO
IF OBJECT_ID(N'[dbo].[SalesOrders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SalesOrders];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [ProductID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [ProductNumber] nvarchar(25)  NOT NULL,
    [Color] nvarchar(15)  NULL,
    [ListPrice] decimal(19,4)  NOT NULL,
    [ProductCategoryID] int  NULL,
    [rowguid] uniqueidentifier  NOT NULL,
    [ModifiedDate] datetime  NOT NULL,
    [ProductDescription] nvarchar(max)  NOT NULL,
    [Quantity] int  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'ProductCategories'
CREATE TABLE [dbo].[ProductCategories] (
    [ProductCategoryID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [rowguid] uniqueidentifier  NOT NULL,
    [ModifiedDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'SalesOrderItems'
CREATE TABLE [dbo].[SalesOrderItems] (
    [SalesOrderItemID] int IDENTITY(1,1) NOT NULL,
    [ProductID] int  NOT NULL,
    [SalesOrderID] int  NOT NULL,
    [OrderBy] int  NOT NULL,
    [Quantity] int  NOT NULL,
    [ActualCost] decimal(19,4)  NOT NULL,
    [ModifiedDate] datetime  NOT NULL,
    [SalesOrderSalesOrderID] int  NOT NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [CustomerID] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [PasswordHash] nvarchar(max)  NOT NULL,
    [PasswordSalt] nvarchar(max)  NOT NULL,
    [IsAdmin] bit  NOT NULL
);
GO

-- Creating table 'SalesOrders'
CREATE TABLE [dbo].[SalesOrders] (
    [SalesOrderID] int IDENTITY(1,1) NOT NULL,
    [CustomerID] int  NOT NULL,
    [OrderDate] datetime  NOT NULL,
    [Customer_CustomerID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ProductID] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([ProductID] ASC);
GO

-- Creating primary key on [ProductCategoryID] in table 'ProductCategories'
ALTER TABLE [dbo].[ProductCategories]
ADD CONSTRAINT [PK_ProductCategories]
    PRIMARY KEY CLUSTERED ([ProductCategoryID] ASC);
GO

-- Creating primary key on [SalesOrderItemID] in table 'SalesOrderItems'
ALTER TABLE [dbo].[SalesOrderItems]
ADD CONSTRAINT [PK_SalesOrderItems]
    PRIMARY KEY CLUSTERED ([SalesOrderItemID] ASC);
GO

-- Creating primary key on [CustomerID] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([CustomerID] ASC);
GO

-- Creating primary key on [SalesOrderID] in table 'SalesOrders'
ALTER TABLE [dbo].[SalesOrders]
ADD CONSTRAINT [PK_SalesOrders]
    PRIMARY KEY CLUSTERED ([SalesOrderID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ProductCategoryID] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Product_ProductCategory_ProductCategoryID]
    FOREIGN KEY ([ProductCategoryID])
    REFERENCES [dbo].[ProductCategories]
        ([ProductCategoryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_ProductCategory_ProductCategoryID'
CREATE INDEX [IX_FK_Product_ProductCategory_ProductCategoryID]
ON [dbo].[Products]
    ([ProductCategoryID]);
GO

-- Creating foreign key on [ProductID] in table 'SalesOrderItems'
ALTER TABLE [dbo].[SalesOrderItems]
ADD CONSTRAINT [FK_TransactionHistory_Product_ProductID]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Products]
        ([ProductID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TransactionHistory_Product_ProductID'
CREATE INDEX [IX_FK_TransactionHistory_Product_ProductID]
ON [dbo].[SalesOrderItems]
    ([ProductID]);
GO

-- Creating foreign key on [Customer_CustomerID] in table 'SalesOrders'
ALTER TABLE [dbo].[SalesOrders]
ADD CONSTRAINT [FK_SalesOrderCustomer]
    FOREIGN KEY ([Customer_CustomerID])
    REFERENCES [dbo].[Customers]
        ([CustomerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SalesOrderCustomer'
CREATE INDEX [IX_FK_SalesOrderCustomer]
ON [dbo].[SalesOrders]
    ([Customer_CustomerID]);
GO

-- Creating foreign key on [SalesOrderSalesOrderID] in table 'SalesOrderItems'
ALTER TABLE [dbo].[SalesOrderItems]
ADD CONSTRAINT [FK_SalesOrderSalesOrderItem]
    FOREIGN KEY ([SalesOrderSalesOrderID])
    REFERENCES [dbo].[SalesOrders]
        ([SalesOrderID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SalesOrderSalesOrderItem'
CREATE INDEX [IX_FK_SalesOrderSalesOrderItem]
ON [dbo].[SalesOrderItems]
    ([SalesOrderSalesOrderID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------