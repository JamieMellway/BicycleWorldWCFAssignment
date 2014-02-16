This is an assignment for a WCF course.  It's a shopping cart app in WPF.

There are scripts in the DatabaseScripts solution folder to help generate the needed database.

Technical Specification
Solution Name: BicycleWorld
Database: New BicycleWorld ModelFirst database on SQL2008R2, loosely based on AdventureWorks OLTP.  It should contain ProductCategory, Product, SalesOrderItem, SalesOrder, and Customer tables.  Data from Products and ProductSubCategories from AdventureWorks is used to populate the BicycleWorld Product and ProductCategory tables respectively.  Customer table will include Username, PasswordHash, PasswordSalt, and IsAdmin columns.
Data Layer: Entity Framework using DBContext.  The entities required are ProductCategories, Products, SalesOrderItems, SalesOrders, and Customers and these should closely match the corresponding tables.   
Business Layer: DataContracts for DTOs and Custom faults  in ObjectModel.  Since encryption is used, the object graphs on encrypted messages should be minimal.  ShoppingCart methods and CRUD methods for Products, Categories, and SalesOrder in Core.  
ShoppingCart check in (creating sales order and decrementing quantity) needs to be in one transaction.  
Service Layer: There will be two services.  The service for products and categories will use basicHttpBinding.  The ShoppingCart and SalesOrder service will be over https on wsHttpContextBinding using the TransportWithMessageCredentials security mode with Username credentials, have a PerSessionInstanceContextMode, have Logging, will do authorization by a customer service behaviour, will use salted hashes stored in the database, use durable sessions, hosted with a WPF host, will use a self-issued certificate, and will use the RepeatableRead transactional level.
Client Layer:  The client will be WPF.  Login screen. Category list and details. Product list and details.  Readonly SaleOrderHistory list and detail.  Shopping Cart main screen.
