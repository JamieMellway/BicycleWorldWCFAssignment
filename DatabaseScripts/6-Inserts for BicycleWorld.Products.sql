SET IDENTITY_INSERT [BicycleWorld].[dbo].[Products] on 
GO

INSERT INTO [BicycleWorld].[dbo].[Products]
( [ProductID]
      ,[Name]
      ,[ProductNumber]
      ,[Color]
      ,[ListPrice]
      ,[ProductCategoryID]
      ,[rowguid]
      ,[ModifiedDate]
      ,[ProductDescription]
      ,[Quantity]
      ,[IsActive]
  )
	SELECT [Product].[ProductID]
		  ,[Product].[Name]
		  ,[ProductNumber]
		  ,[Color]
		  ,[ListPrice]
		  ,[ProductSubcategoryID] as [ProductCategoryID]
		  ,[Product].[rowguid]
		  ,[Product].[ModifiedDate]
		  ,[ProductDescription].[Description] as [ProductDescription]
		  ,isnull(sum(Quantity),0) as [Quantity]
		  ,1
	  FROM [AdventureWorks].[Production].[Product]
	LEFT JOIN [AdventureWorks].[Production].[ProductModel] on [Product].[ProductModelID] = [ProductModel].[ProductModelID]
	LEFT JOIN [AdventureWorks].[Production].[ProductModelProductDescriptionCulture] on [ProductModelProductDescriptionCulture].ProductModelID = [ProductModel].[ProductModelID]
	LEFT JOIN [AdventureWorks].[Production].[ProductDescription] on [ProductModelProductDescriptionCulture].[ProductDescriptionID] = [ProductDescription].[ProductDescriptionID]
	LEFT JOIN [AdventureWorks].[Production].[ProductInventory] on [Product].[ProductID] = [ProductInventory].[ProductID]
	WHERE [ProductModelProductDescriptionCulture].CultureID = 'en'
	GROUP BY
		   [Product].[ProductID]
		  ,[Product].[Name]
		  ,[ProductNumber]
		  ,[Color]
		  ,[ListPrice]
		  ,[ProductSubcategoryID]
		  ,[Product].[rowguid]
		  ,[Product].[ModifiedDate]
		  ,[ProductDescription].[Description]
GO

SET IDENTITY_INSERT [BicycleWorld].[dbo].[Products] off 
GO