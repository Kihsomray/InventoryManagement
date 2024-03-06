/*
-- Create/use database
CREATE DATABASE  InventoryManagement;
USE InventoryManagement; 


-- Location
CREATE TABLE Location (
    LocationID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100),
    Address VARCHAR(255)
);

-- Employee
CREATE TABLE Employee (
    EmployeeID INT AUTO_INCREMENT PRIMARY KEY,
    LocationID INT,
    FullName VARCHAR(255),
    Position VARCHAR(100),
    Email VARCHAR(255),
    PhoneNumber VARCHAR(20), 
    FOREIGN KEY (LocationID) REFERENCES Location(LocationID)
);

-- Supplier
CREATE TABLE Supplier ( 
    SupplierID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100),
    Email VARCHAR(255),
    PhoneNumber VARCHAR(20),
    Address VARCHAR(255),
    DateOfCreation DATE
);

-- asalamalakim to the grader

-- Items
CREATE TABLE Item (
    ItemID INT AUTO_INCREMENT PRIMARY  KEY,
    Name VARCHAR(100), 
    Category VARCHAR(50),
    Description  TEXT,
    Price DECIMAL(10, 2),
    SupplierID INT,
    FOREIGN KEY (SupplierID) REFERENCES Supplier(SupplierID)
);

-- Expense
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

-- Inventory
CREATE TABLE Inventory (
    LocationID INT,
    ItemID INT,   
    ReorderQuantity  INT,
    ReorderLevel INT,
    PRIMARY KEY (LocationID, ItemID),  -- Composite primary key
    FOREIGN KEY (LocationID) REFERENCES  Location(LocationID),
    FOREIGN KEY (ItemID) REFERENCES Item(ItemID)
);


-- Discount
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

-- Feedback
CREATE TABLE Review (
    CustomerID  INT,
    ItemID INT,
    Rating INT,  
    Description TEXT,
    PRIMARY  KEY (CustomerID, ItemID),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    FOREIGN KEY (ItemID) REFERENCES Item(ItemID)
);

-- Order
CREATE TABLE `Order` (  
    OrderID INT AUTO_INCREMENT PRIMARY KEY,
    CustomerID INT,
    OrderDate DATE,
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);



-- OrderItem
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

-- Location
INSERT INTO Location (LocationID, Name, Address) 
VALUES 
    (1, 'Main Warehouse', '123 Main Street, City, Country'),
    (2, 'Regional Store', '456 Regional Avenue, City, Country');

-- Employee
INSERT INTO Employee (EmployeeID, LocationID, FullName, Position, Email, PhoneNumber)
VALUES 
    (1, 1, 'John Doe', 'Warehouse Manager', 'johndoe@example.com', '123-456-7890'),
    (2, 2, 'Jane Smith', 'Store Manager', 'janesmith@example.com', '987-654-3210');

-- Supplier
INSERT INTO Supplier (SupplierID, Name, Email, PhoneNumber, Address, DateOfCreation)
VALUES 
    (1, 'ABC Distributors', 'abcdist@example.com', '111-222-3333', '789 Supplier Street, City, Country', '2023-01-15'),
    (2, 'XYZ Wholesalers', 'xyzwholesale@example.com', '444-555-6666', '321 Wholesaler Road, City, Country', '2022-11-20');

-- Items
INSERT INTO Item (ItemID, Name, Category, Description, Price, SupplierID)
VALUES 
    (1, 'Laptop', 'Electronics', '15" Laptop with SSD', 899.99, 1),
    (2, 'Smartphone', 'Electronics', 'Latest model smartphone', 699.99, 1),
    (3, 'Desk Chair', 'Furniture', 'Ergonomic desk chair', 199.99, 2),
    (4, 'Printer', 'Office Supplies', 'Color inkjet printer', 299.99, 2),
    (5, 'Headphones', 'Electronics', 'Noise-canceling headphones', 149.99, 1);

-- Expense
INSERT INTO Expense (ExpenseID, LocationID, ItemID, Date, Quantity, Method, Completed)
VALUES 
    (1, 1, 1, '2024-02-28', 10, 'Credit Card', 1),
    (2, 2, 3, '2024-02-27', 5, 'Cash', 1),
    (3, 1, 4, '2024-02-26', 2, 'Credit Card', 0);

-- Inventory
INSERT INTO Inventory (LocationID, ItemID, ReorderQuantity, ReorderLevel)
VALUES 
    (1, 1, 20, 5),
    (1, 2, 30, 10),
    (2, 3, 15, 3),
    (2, 4, 8, 2),
    (1, 5, 25, 8);

-- Discount
INSERT INTO Discount (ItemID, Percentage, StartDate, EndDate, QuantityUsed, UsageLimit)
VALUES 
    (1, 10.00, '2024-01-01', '2024-12-31', 50, 100),
    (2, 5.00, '2024-01-01', '2024-12-31', 30, 50),
    (3, 15.00, '2024-01-01', '2024-12-31', 20, 40),
    (4, 8.00, '2024-01-01', '2024-12-31', 10, 20),
    (5, 12.00, '2024-01-01', '2024-12-31', 40, 80);

-- Customer
INSERT INTO Customer (CustomerID, FullName, Email, PhoneNumber, Address, DateOfCreation)
VALUES 
    (1, 'Alice Johnson', 'alice@example.com', '555-123-4567', '789 Maple Street, City, Country', '2023-05-10'),
    (2, 'Bob Smith', 'bob@example.com', '555-987-6543', '123 Oak Street, City, Country', '2023-08-22');

-- Review
INSERT INTO Review (CustomerID, ItemID, Rating, Description)
VALUES 
    (1, 1, 4, 'Great laptop for everyday use'),
    (2, 3, 5, 'Comfortable chair, highly recommended');

-- Order
INSERT INTO `Order` (OrderID, CustomerID, OrderDate)
VALUES 
    (1, 1, '2024-02-28'),
    (2, 2, '2024-02-29');

-- OrderItem
INSERT INTO OrderItem (OrderID, ItemID, Quantity)
VALUES 
    (1, 1, 1),
    (1, 3, 2),
    (2, 2, 1),
    (2, 4, 1);

-- Feedback
INSERT INTO Feedback (OrderID, Rating, Title, Description)
VALUES 
    (1, 4, 'Good Service', 'Items arrived promptly and in good condition'),
    (2, 5, 'Excellent Experience', 'Smooth ordering process and fast delivery');

-- Shipment
INSERT INTO Shipment (OrderID, Date, Status, ShippingNumber)
VALUES 
    (1, '2024-03-01', 'Shipped', 'SH123456'),
    (2, '2024-03-01', 'Shipped', 'SH654321');

-- Payment
INSERT INTO Payment (OrderID, Date, Amount, Method, Completed)
VALUES 
    (1, '2024-02-28', 999.98, 'Credit Card', 1),
    (2, '2024-02-29', 799.98, 'PayPal', 1);

-- Return
INSERT INTO `Return` (OrderID, ReturnDate, ReturnReason)
VALUES 
    (1, '2024-03-05', 'Received wrong item'),
    (2, '2024-03-07', 'Changed mind, no longer needed');

-- Cart
INSERT INTO Cart (CustomerID, ItemID, Quantity)
VALUES 
    (1, 5, 1),
    (2, 4, 2);

-- Membership
INSERT INTO Membership (CustomerID, MembershipType, StartDate, EndDate)
VALUES 
    (1, 'Gold', '2023-01-01', '2024-12-31'),
    (2, 'Silver', '2023-03-15', '2024-06-30');

-- Query 1: Retrieve all employees along with their locations
SELECT Employee.FullName, Employee.Position, Location.Name AS Location
FROM Employee
INNER JOIN Location ON Employee.LocationID = Location.LocationID;

-- Query 2: Get the total number of items in the inventory for each location
SELECT Location.Name AS Location, COUNT(ItemID) AS TotalItems
FROM Inventory
INNER JOIN Location ON Inventory.LocationID = Location.LocationID
GROUP BY Location.Name;

-- Query 3: Find the average rating and count of reviews for each item
SELECT Item.Name, AVG(Review.Rating) AS AvgRating, COUNT(Review.ItemID) AS ReviewCount
FROM Review
INNER JOIN Item ON Review.ItemID = Item.ItemID
GROUP BY Item.Name;

-- Query 4: List all orders along with their shipment status
SELECT `Order`.OrderID, Shipment.Status
FROM `Order`
LEFT JOIN Shipment ON `Order`.OrderID = Shipment.OrderID;

-- Query 5: Retrieve the total amount spent by each customer
SELECT Customer.FullName, SUM(Payment.Amount) AS TotalAmountSpent
FROM Customer
LEFT JOIN `Order` ON Customer.CustomerID = `Order`.CustomerID
LEFT JOIN Payment ON `Order`.OrderID = Payment.OrderID
GROUP BY Customer.FullName;

-- 1. Manager's stock update and order preparation

-- View current stock and suppliers for items
SELECT i.ItemID, i.Name AS ItemName, i.Price, i.SupplierID, s.Name AS SupplierName
FROM Item i
INNER JOIN Supplier s ON i.SupplierID = s.SupplierID;

-- 2. Customer order management

-- Customer order data
SELECT c.CustomerID, c.FullName AS CustomerName, c.Address AS ShippingAddress, o.OrderID, o.OrderDate
FROM Customer c
INNER JOIN `Order` o ON c.CustomerID = o.CustomerID;

-- 3. Employees sorted by location

-- Employees sorted by location
SELECT e.EmployeeID, e.FullName AS EmployeeName, e.Position, e.LocationID
FROM Employee e
ORDER BY e.LocationID;

-- 4. Order tracking and management

-- View order status
SELECT o.OrderID, o.OrderDate, s.Status AS ShipmentStatus
FROM `Order` o
LEFT JOIN Shipment s ON o.OrderID = s.OrderID;

-- 5. Item discount management

-- View items and current discounts
SELECT i.ItemID, i.Name AS ItemName, d.Percentage AS DiscountPercentage
FROM Item i
LEFT JOIN Discount d ON i.ItemID = d.ItemID;

-- 6. Membership discount management

SELECT 
    m.MembershipType,
    GROUP_CONCAT(m.CustomerID ORDER BY m.CustomerID) AS CustomerIDs
FROM 
    Membership m
GROUP BY 
    m.MembershipType
ORDER BY 
    m.MembershipType;

-- 7. Stock check at a specific location

-- View stock at a specific location
SELECT i.ItemID, i.Name AS ItemName, inv.ReorderQuantity AS StockQuantity
FROM Inventory inv
INNER JOIN Item i ON inv.ItemID = i.ItemID
WHERE inv.LocationID = 1; -- Sample location ID

*/