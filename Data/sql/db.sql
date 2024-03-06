/* Create/use database */
CREATE DATABASE  InventoryManagement;
USE InventoryManagement; 


/* Location */
CREATE TABLE Location (
    LocationID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100),
    Address VARCHAR(255)
);

/* Employee */
CREATE TABLE Employee (
    EmployeeID INT AUTO_INCREMENT PRIMARY KEY,
    LocationID INT,
    FullName VARCHAR(255),
    Position VARCHAR(100),
    Email VARCHAR(255),
    PhoneNumber VARCHAR(20), 
    FOREIGN KEY (LocationID) REFERENCES Location(LocationID)
);

/* Supplier */
CREATE TABLE Supplier ( 
    SupplierID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100),
    Email VARCHAR(255),
    PhoneNumber VARCHAR(20),
    Address VARCHAR(255),
    DateOfCreation DATE
);

/* asalamalakim to the grader */

/* Items */
CREATE TABLE Item (
    ItemID INT AUTO_INCREMENT PRIMARY  KEY,
    Name VARCHAR(100), 
    Category VARCHAR(50),
    Description  TEXT,
    Price DECIMAL(10, 2),
    SupplierID INT,
    FOREIGN KEY (SupplierID) REFERENCES Supplier(SupplierID)
);

/* Expense */
CREATE TABLE Expense ( 
    ExpenseID INT AUTO_INCREMENT PRIMARY KEY,
    LocationID INT,
    ItemID INT,
    Date DATE,
    Quantity INT,
    Method VARCHAR(50),
    Completed BIT,
    FOREIGN KEY (LocationID) REFERENCES Location(LocationID),
    FOREIGN KEY (ItemID) REFERENCES Item(ItemID)
);

/* Inventory */
CREATE TABLE Inventory (
    LocationID INT,
    ItemID INT,   
    ReorderQuantity  INT,
    ReorderLevel INT,
    PRIMARY KEY (LocationID, ItemID),  -- Composite primary key
    FOREIGN KEY (LocationID) REFERENCES  Location(LocationID),
    FOREIGN KEY (ItemID) REFERENCES Item(ItemID)
);


/* Discount */
CREATE TABLE Discount (
    ItemID INT,
    Percentage DECIMAL(5, 2),
    StartDate DATE,
    EndDate DATE,
    QuantityUsed INT,
    UsageLimit INT,
    PRIMARY KEY (ItemID),
    FOREIGN KEY (ItemID) REFERENCES Item(ItemID)
);

CREATE TABLE Customer (
    CustomerID INT AUTO_INCREMENT PRIMARY KEY,
    FullName VARCHAR(255),
    Email VARCHAR(255),
    PhoneNumber VARCHAR(20),
    Address VARCHAR(255),
    DateOfCreation DATE
);

/* Feedback */
CREATE TABLE Review (
    CustomerID  INT,
    ItemID INT,
    Rating INT,  
    Description TEXT,
    PRIMARY  KEY (CustomerID, ItemID),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    FOREIGN KEY (ItemID) REFERENCES Item(ItemID)
);

/* Order */
CREATE TABLE `Order` (  
    OrderID INT AUTO_INCREMENT PRIMARY KEY,
    CustomerID INT,
    OrderDate DATE,
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);



/* OrderItem */
CREATE TABLE  OrderItem (
    OrderID INT,
    ItemID INT,
    Quantity INT,
    PRIMARY KEY (OrderID, ItemID),
    FOREIGN KEY (OrderID) REFERENCES `Order`(OrderID),
    FOREIGN KEY (ItemID) REFERENCES Item(ItemID)
);

CREATE TABLE Feedback (
    OrderID INT,
    Rating INT,
    Title VARCHAR(100),
    Description TEXT,  
    PRIMARY KEY (OrderID),
	FOREIGN KEY (OrderID) REFERENCES `Order`(OrderID)
);
  
CREATE TABLE Shipment (
    OrderID INT,
    Date DATE,
  Status VARCHAR(50),
    ShippingNumber VARCHAR(20),
    PRIMARY KEY (OrderID),
    FOREIGN KEY  (OrderID) REFERENCES `Order`(OrderID)
);

CREATE TABLE Payment (
    OrderID INT,
    Date DATE,
    Amount DECIMAL(10, 2),
    Method VARCHAR(50),
    Completed BOOLEAN,
    PRIMARY KEY (OrderID),
   FOREIGN KEY (OrderID) REFERENCES `Order`(OrderID)
);

CREATE TABLE  `Return` (  
    OrderID INT,
    ReturnDate DATE,
    ReturnReason TEXT,
    PRIMARY KEY (OrderID),
    FOREIGN KEY (OrderID) REFERENCES `Order`(OrderID)
);

CREATE TABLE Cart (
    CustomerID INT,
    ItemID INT,
    Quantity INT,
    PRIMARY  KEY (CustomerID, ItemID),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    FOREIGN KEY (ItemID) REFERENCES Item(ItemID)
);


CREATE TABLE Membership (
    CustomerID   INT PRIMARY KEY,
    MembershipType VARCHAR(50),  
    StartDate DATE,
    EndDate DATE,
    FOREIGN  KEY (CustomerID) REFERENCES Customer(CustomerID)
);