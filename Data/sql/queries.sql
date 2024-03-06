/* Query 1: Retrieve all employees along with their locations */
SELECT Employee.FullName, Employee.Position, Location.Name AS Location
FROM Employee
INNER JOIN Location ON Employee.LocationID = Location.LocationID;

/* Query 2: Get the total number of items in the inventory for each location */
SELECT Location.Name AS Location, COUNT(ItemID) AS TotalItems
FROM Inventory
INNER JOIN Location ON Inventory.LocationID = Location.LocationID
GROUP BY Location.Name;

/* Query 3: Find the average rating and count of reviews for each item */
SELECT Item.Name, AVG(Review.Rating) AS AvgRating, COUNT(Review.ItemID) AS ReviewCount
FROM Review
INNER JOIN Item ON Review.ItemID = Item.ItemID
GROUP BY Item.Name;

/* Query 4: List all orders along with their shipment status */
SELECT `Order`.OrderID, Shipment.Status
FROM `Order`
LEFT JOIN Shipment ON `Order`.OrderID = Shipment.OrderID;

/* Query 5: Retrieve the total amount spent by each customer */
SELECT Customer.FullName, SUM(Payment.Amount) AS TotalAmountSpent
FROM Customer
LEFT JOIN `Order` ON Customer.CustomerID = `Order`.CustomerID
LEFT JOIN Payment ON `Order`.OrderID = Payment.OrderID
GROUP BY Customer.FullName;

/* 1. Manager's stock update and order preparation */

-- View current stock and suppliers for items
SELECT i.ItemID, i.Name AS ItemName, i.Price, i.SupplierID, s.Name AS SupplierName
FROM Item i
INNER JOIN Supplier s ON i.SupplierID = s.SupplierID;

/* 2. Customer order management */

-- Customer order data
SELECT c.CustomerID, c.FullName AS CustomerName, c.Address AS ShippingAddress, o.OrderID, o.OrderDate
FROM Customer c
INNER JOIN `Order` o ON c.CustomerID = o.CustomerID;

/* 3. Employees sorted by location */

-- Employees sorted by location
SELECT e.EmployeeID, e.FullName AS EmployeeName, e.Position, e.LocationID
FROM Employee e
ORDER BY e.LocationID;

/* 4. Order tracking and management */

-- View order status
SELECT o.OrderID, o.OrderDate, s.Status AS ShipmentStatus
FROM `Order` o
LEFT JOIN Shipment s ON o.OrderID = s.OrderID;

/* 5. Item discount management */

-- View items and current discounts
SELECT i.ItemID, i.Name AS ItemName, d.Percentage AS DiscountPercentage
FROM Item i
LEFT JOIN Discount d ON i.ItemID = d.ItemID;

/* 6. Membership discount management */

SELECT 
    m.MembershipType,
    GROUP_CONCAT(m.CustomerID ORDER BY m.CustomerID) AS CustomerIDs
FROM 
    Membership m
GROUP BY 
    m.MembershipType
ORDER BY 
    m.MembershipType;

/* 7. Stock check at a specific location */

-- View stock at a specific location
SELECT i.ItemID, i.Name AS ItemName, inv.ReorderQuantity AS StockQuantity
FROM Inventory inv
INNER JOIN Item i ON inv.ItemID = i.ItemID
WHERE inv.LocationID = 1; -- Sample location ID