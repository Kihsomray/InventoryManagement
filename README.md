<img src="https://raw.githubusercontent.com/Kihsomray/InventoryManagement/master/wwwroot/images/database-logo.png" width="12%" height="12%"><p></p> 
# TCSS 445 - Course Project | Inventory Management System
### Authors
- Michael Yarmoshik
- Timofej Ratsko
## About
We are developing an Inventory Management System (IMS) for our online and in-person business that sells groceries, health products, clothing, tools, office materials, and home goods. We are connecting suppliers with customers and organizing any purchases or expenses made in our business. This system will allow for greater efficiency and consistency in our business by streamlining the process with the use of a database system. We strive to create community and quality products for an affordable price, whilst creating a challenge for other companies trying to do the same.

<img src="https://raw.githubusercontent.com/Kihsomray/InventoryManagement/master/wwwroot/images/inventory-management-dark.png"><p></p>

## Installation
1. Install [.NET 7 core](https://learn.microsoft.com/en-us/dotnet/core/install/).
2. Extract files, navigate to the base directory.
3. Add database information in `Data > InventoryManagementContext.cs > _connectionString`.
4. Run SQL queries in `Data > sql` in your DBMS:
    - first run `db.sql` to create the database and tables.
    - second run `data.sql` to insert sample data.
5. Run `dotnet run` from the root directory of the project.