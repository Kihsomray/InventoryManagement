/* Location */
INSERT INTO Location (Name, Address) 
VALUES 
    ('Main Warehouse', '123 Main Street, City, Country'),
    ('Regional Store', '456 Regional Avenue, City, Country');

/* Employee */
INSERT INTO Employee (LocationID, FullName, Position, Email, PhoneNumber)
VALUES 
    (1, 'John Doe', 'Warehouse Manager', 'johndoe@example.com', '123-456-7890'),
    (2, 'Jane Smith', 'Store Manager', 'janesmith@example.com', '987-654-3210');

/* Supplier */
INSERT INTO Supplier (Name, Email, PhoneNumber, Address, DateOfCreation)
VALUES 
    ('ABC Distributors', 'abcdist@example.com', '111-222-3333', '789 Supplier Street, City, Country', '2023-01-15'),
    ('XYZ Wholesalers', 'xyzwholesale@example.com', '444-555-6666', '321 Wholesaler Road, City, Country', '2022-11-20');

/* Items */
INSERT INTO Item (Name, Category, Description, Price, SupplierID)
VALUES 
    ('Laptop', 'Electronics', '15" Laptop with SSD', 899.99, 1),
    ('Smartphone', 'Electronics', 'Latest model smartphone', 699.99, 1),
    ('Desk Chair', 'Furniture', 'Ergonomic desk chair', 199.99, 2),
    ('Printer', 'Office Supplies', 'Color inkjet printer', 299.99, 2),
    ('Headphones', 'Electronics', 'Noise-canceling headphones', 149.99, 1);

/* Expense */
INSERT INTO Expense (LocationID, ItemID, Date, Quantity, Method, Completed)
VALUES 
    (1, 1, '2024-02-28', 10, 'Credit Card', 1),
    (2, 3, '2024-02-27', 5, 'Cash', 1),
    (1, 4, '2024-02-26', 2, 'Credit Card', 0);

/* Inventory */
INSERT INTO Inventory (LocationID, ItemID, ReorderQuantity, ReorderLevel)
VALUES 
    (1, 1, 20, 5),
    (1, 2, 30, 10),
    (2, 3, 15, 3),
    (2, 4, 8, 2),
    (1, 5, 25, 8);

/* Discount */
INSERT INTO Discount (ItemID, Percentage, StartDate, EndDate, QuantityUsed, UsageLimit)
VALUES 
    (1, 10.00, '2024-01-01', '2024-12-31', 50, 100),
    (2, 5.00, '2024-01-01', '2024-12-31', 30, 50),
    (3, 15.00, '2024-01-01', '2024-12-31', 20, 40),
    (4, 8.00, '2024-01-01', '2024-12-31', 10, 20),
    (5, 12.00, '2024-01-01', '2024-12-31', 40, 80);

/* Customer */
INSERT INTO Customer (FullName, Email, PhoneNumber, Address, DateOfCreation)
VALUES 
    ('Alice Johnson', 'alice@example.com', '555-123-4567', '789 Maple Street, City, Country', '2023-05-10'),
    ('Bob Smith', 'bob@example.com', '555-987-6543', '123 Oak Street, City, Country', '2023-08-22');

/* Review */
INSERT INTO Review (CustomerID, ItemID, Rating, Description)
VALUES 
    (1, 1, 4, 'Great laptop for everyday use'),
    (2, 3, 5, 'Comfortable chair, highly recommended');

/* Order */
INSERT INTO `Order` (CustomerID, OrderDate)
VALUES 
    (1, '2024-02-28'),
    (2, '2024-02-29');

/* OrderItem */
INSERT INTO OrderItem (OrderID, ItemID, Quantity)
VALUES 
    (1, 1, 1),
    (1, 3, 2),
    (2, 2, 1),
    (2, 4, 1);

/* Feedback */
INSERT INTO Feedback (OrderID, Rating, Title, Description)
VALUES 
    (1, 4, 'Good Service', 'Items arrived promptly and in good condition'),
    (2, 5, 'Excellent Experience', 'Smooth ordering process and fast delivery');

/* Shipment */
INSERT INTO Shipment (OrderID, Date, Status, ShippingNumber)
VALUES 
    (1, '2024-03-01', 'Shipped', 'SH123456'),
    (2, '2024-03-01', 'Shipped', 'SH654321');

/* Payment */
INSERT INTO Payment (OrderID, Date, Amount, Method, Completed)
VALUES 
    (1, '2024-02-28', 999.98, 'Credit Card', 1),
    (2, '2024-02-29', 799.98, 'PayPal', 1);

/* Return */
INSERT INTO `Return` (OrderID, ReturnDate, ReturnReason)
VALUES 
    (1, '2024-03-05', 'Received wrong item'),
    (2, '2024-03-07', 'Changed mind, no longer needed');

/* Cart */
INSERT INTO Cart (CustomerID, ItemID, Quantity)
VALUES 
    (1, 5, 1),
    (2, 4, 2);

/* Membership */
INSERT INTO Membership (CustomerID, MembershipType, StartDate, EndDate)
VALUES 
    (1, 'Gold', '2023-01-01', '2024-12-31'),
    (2, 'Silver', '2023-03-15', '2024-06-30');
