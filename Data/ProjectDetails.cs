/*
Inventory Management System - Proposal
Timofej Ratsko and Michael Yarmoshik

1. Introduction
We are developing an Inventory Management System (IMS) for our online and in-person business that sells groceries, health products, clothing, tools, office materials, and home goods. We are connecting suppliers with customers and organizing any purchases or expenses made in our business. This system will allow for greater efficiency and consistency in our business by streamlining the process with the use of a database system. We strive to create community and quality products for an affordable price, whilst creating a challenge for other companies trying to do the same.

2. Database data
This system will keep track of:
1. Item: ID, supplier ID, name, description, sale price, units in stock
2. Item category: ID, name
3. Customer: ID, first name, last name, email, phone number, street address, city, state, zip code, country, number of items purchased, date of account creation
4. Supplier: ID, name, contact name, contact email, contact phone, street address, city, state, zip, country, Items supplied
5. Employee: ID, first name, last name, position, email, phone, employed
6. Report: ID, customer ID, rating, reason
7. Order: ID, product ID, customer ID, payment ID, order date, quantity, total price
8. Cart Item: ID, product ID, customer ID, quantity, subtotal price
9. Inventory: product ID, purchase price, reorder quantity, reorder level
10. Payment: ID, order ID, date, quantity, method, completed
11. Expense: ID, item ID, date, quantity, method, completed         
12. Shipment: ID, order ID, date, status, shipping number
13. Discount/Promotion: ID, product ID, percentage, start date, end date, quantity used, usage limit
14. Feedback: ID, order ID, customer ID, rating, title, description
15. Membership: ID, customer ID, type, start date, end date
16. Return: ID, order ID, customer ID, date returned, reason
17. Location: ID, name, street address, city, state, zip code, country

3. Scenarios
A few
of the typical scenarios we will encounter are:
The manager just got some stock at his warehouse and wants to input the items he ordered from the supplier. He can look at all of the items that are already in stock and see what he needs before he orders as well as see how much it would cost and which supplier he needs to order from
A customer wants to order from the store. The company needs to manage data from the customer to know where to ship the items.
A manager wants to analyze his employees so he can make a report and analyze his performance.
The company wants to keep track of orders to see if they are going successfully or if something has to be done incase of a failed shipment or lost items.
The company wants to put a discount on an item that they think people would buy more of if it were at a cheaper price.
The company wants to give all members a discount on a specific time so they would grab all members and give them a discount.
The company wants to check the stock at a specific location to see how we are doing for the stock of the items at that location and to see if we need more.
 
4. Analytical queries
Some examples of queries that provide interesting information are:
1. Find the most common item to be found within all of the shopping carts. This is to see what is the most in-demand item and with this, we can put a time-sensitive discount to increase sales.
2. Find out the most loyal customers, the top few (5-50) customers with the greatest number of items bought and longest held membership to reward them with something for free or some discount on the entire store.
3. Find our most sold items to increase the price of them. They have a great demand so we should increase the price to reap the rewards.
4. Find our most returned item to see if we should think of getting rid of it or we should replace it with some other item.
5. Find our largest expenses to keep track of where we are losing the most money.

5. Business logic
In terms of customers, we should not have any duplicate memberships for any one customer. The membership will not allow for a customer with an existing membership to create a new one.
For our items, each item should only be in one item category for the sake of consistency so if we are adding a new item to our stock it will not be allowed to be in two or more categories but will require one and only one category.
Regarding discounts, each item should only have one discount. If multiple are created for an item, the greater discount will be the only one that is taken into effect.
When it comes to returns, each order can only be returned once. If it is returned multiple times the first return is the only one that will be processed.
Taking the cart into account, there shouldn’t be two of the same item for one customer. Instead, the quantity should only increase if another one of that item is added to the cart.
When hiring employees, there shouldn't be duplicate items if one gets rehired. Instead, the system should recognize the previous time the employee had worked there.
*/