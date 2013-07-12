SET IDENTITY_INSERT [BicycleWorld].[dbo].[ProductCategories] on 
GO

INSERT INTO [BicycleWorld].[dbo].[ProductCategories]
           ([ProductCategoryID]
           ,[Name]
		   ,[IsActive]
           ,[rowguid]
           ,[ModifiedDate])
SELECT [ProductSubcategoryID]
      ,[Name]
	  ,1
      ,[rowguid]
      ,[ModifiedDate]
  FROM [AdventureWorks].[Production].[ProductSubcategory]
GO  

SET IDENTITY_INSERT [BicycleWorld].[dbo].[ProductCategories] off
go
