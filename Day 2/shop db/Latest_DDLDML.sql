/*
Design the database for a shop which sells products
Points for consideration
  1) One product can be supplied by many suppliers
  2) One supplier can supply many products
  3) All customers details have to present
  4) A customer can buy more than one product in every purchase
  5) Bill for every purchase has to be stored
  6) These are just details of one shop
*/
 
/*

Schema:

categories
id, name, status
 
country
id, name
 
state
id, name, country_id
 
City
id, name, state_id
 
area
zipcode, name, city_id
 
address
id, door_number, addressline1, zipcode
 
supplier
id, name, contact_person, phone, email, address_id, status
 
product
id, Name, unit_price, quantity, description, image
 
product_supplier
transaction_id, product_id, supplier_id, date_of_supply, quantity,
 
Customer
id, Name, Phone, age, address_id
 
order
  order_number, customer_id, Date_of_order, amount, order_status
 
order_details
  id, order_number, product_id, quantity, unit_price

*/

/* DDL Commands*/

/* DROP Existing Tables in correct order to avoid FK conflicts */
-- Drop child tables first
DROP TABLE IF EXISTS PurchaseDetails;
DROP TABLE IF EXISTS ProductSupplier;
DROP TABLE IF EXISTS Purchase;
DROP TABLE IF EXISTS Orders;
DROP TABLE IF EXISTS Order_Details;

-- Then drop tables with FK references
DROP TABLE IF EXISTS Product;
DROP TABLE IF EXISTS Supplier;
DROP TABLE IF EXISTS Customer;

-- Finally, drop auxiliary/location tables
DROP TABLE IF EXISTS Address;
DROP TABLE IF EXISTS Area;
DROP TABLE IF EXISTS City;
DROP TABLE IF EXISTS State;
DROP TABLE IF EXISTS Country;
DROP TABLE IF EXISTS Categories;



-- 1. Categories
CREATE TABLE Categories (
    id INT PRIMARY KEY,
    name VARCHAR(100) NOT NULL UNIQUE,
    status VARCHAR(20) CHECK (status IN ('Active', 'Inactive')) NOT NULL
);

-- 2. Country
CREATE TABLE Country (
    id INT PRIMARY KEY,
    name VARCHAR(100) NOT NULL UNIQUE
);

-- 3. State
CREATE TABLE State (
    id INT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    country_id INT NOT NULL,
    FOREIGN KEY (country_id) REFERENCES Country(id)
);

-- 4. City
CREATE TABLE City (
    id INT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    state_id INT NOT NULL,
    FOREIGN KEY (state_id) REFERENCES State(id)
);

-- 5. Area
CREATE TABLE Area (
    zipcode CHAR(6) PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    city_id INT NOT NULL,
    FOREIGN KEY (city_id) REFERENCES City(id)
);

-- 6. Address
CREATE TABLE Address (
    id INT PRIMARY KEY,
    door_number VARCHAR(20) NOT NULL,
    addressline1 VARCHAR(255) NOT NULL,
    zipcode CHAR(6) NOT NULL,
    FOREIGN KEY (zipcode) REFERENCES Area(zipcode)
);

-- 7. Supplier
CREATE TABLE Supplier (
    id INT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    contact_person VARCHAR(100),
    phone VARCHAR(15),
    email VARCHAR(100),
    address_id INT NOT NULL,
    status VARCHAR(20) CHECK (status IN ('Active', 'Inactive')) NOT NULL,
    FOREIGN KEY (address_id) REFERENCES Address(id)
);

-- 8. Product
CREATE TABLE Product (
    id INT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    unit_price DECIMAL(10, 2) NOT NULL CHECK (unit_price >= 0),
    quantity INT NOT NULL CHECK (quantity >= 0),
    description TEXT,
    image VARCHAR(255)
);

-- 9. ProductSupplier (many-to-many with supply details)
CREATE TABLE ProductSupplier (
    transaction_id INT PRIMARY KEY,
    product_id INT NOT NULL,
    supplier_id INT NOT NULL,
    date_of_supply DATE NOT NULL,
    quantity INT NOT NULL CHECK (quantity > 0),
    FOREIGN KEY (product_id) REFERENCES Product(id),
    FOREIGN KEY (supplier_id) REFERENCES Supplier(id)
);

-- 10. Customer
CREATE TABLE Customer (
    id INT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    phone VARCHAR(15),
    age INT CHECK (age >= 0),
    address_id INT NOT NULL,
    FOREIGN KEY (address_id) REFERENCES Address(id)
);

-- 11. Orders
CREATE TABLE Orders (
    order_number INT PRIMARY KEY,
    customer_id INT NOT NULL,
    date_of_order DATE NOT NULL,
    amount DECIMAL(10, 2) NOT NULL CHECK (amount >= 0),
    order_status VARCHAR(20) CHECK (order_status IN ('Pending', 'Completed', 'Cancelled')) NOT NULL,
    FOREIGN KEY (customer_id) REFERENCES Customer(id)
);

-- 12. Order Details
CREATE TABLE Order_Details (
    id INT PRIMARY KEY,
    order_number INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL CHECK (quantity > 0),
    unit_price DECIMAL(10, 2) NOT NULL CHECK (unit_price >= 0),
    FOREIGN KEY (order_number) REFERENCES Orders(order_number),
    FOREIGN KEY (product_id) REFERENCES Product(id)
);

/* DML Commands*/

INSERT INTO Categories (id, name, status)
VALUES 
(1, 'Stationery', 'Active'),
(2, 'Electronics', 'Active'),
(3, 'Furniture', 'Inactive');


INSERT INTO Country (id, name)
VALUES 
(1, 'India'),
(2, 'USA');


INSERT INTO State (id, name, country_id)
VALUES 
(1, 'Tamil Nadu', 1),
(2, 'California', 2);



INSERT INTO City (id, name, state_id)
VALUES 
(1, 'Chennai', 1),
(2, 'Los Angeles', 2);


INSERT INTO Area (zipcode, name, city_id)
VALUES 
('600001', 'T Nagar', 1),
('90001', 'Downtown LA', 2);


INSERT INTO Address (id, door_number, addressline1, zipcode)
VALUES 
(1, '12A', 'Bazaar Street', '600001'),
(2, '221B', 'Main Avenue', '90001');


INSERT INTO Supplier (id, name, contact_person, phone, email, address_id, status)
VALUES 
(1, 'ABC Distributors', 'Ravi Kumar', '9876543210', 'abc@example.com', 1, 'Active'),
(2, 'Global Traders', 'Manoj Kumar', '1234567890', 'global@example.com', 2, 'Active');


INSERT INTO Product (id, name, unit_price, quantity, description, image)
VALUES 
(1, 'Notebook', 20.00, 100, '200-page ruled notebook', 'notebook.jpg'),
(2, 'Ball Pen', 5.00, 300, 'Blue ink pen', 'pen.jpg'),
(3, 'Desk Lamp', 150.00, 50, 'LED desk lamp with USB port', 'lamp.jpg');


INSERT INTO ProductSupplier (transaction_id, product_id, supplier_id, date_of_supply, quantity)
VALUES 
(1, 1, 1, '2025-04-01', 100),
(2, 2, 1, '2025-04-01', 200),
(3, 3, 2, '2025-04-05', 50);


INSERT INTO Customer (id, name, phone, age, address_id)
VALUES 
(1, 'Ariana Grande', '9998877665', 30, 1),
(2, 'Virat Kohli', '8887766554', 35, 2);


INSERT INTO Orders (order_number, customer_id, date_of_order, amount, order_status)
VALUES 
(1001, 1, '2025-05-06', 45.00, 'Completed'),
(1002, 2, '2025-05-07', 155.00, 'Pending');


INSERT INTO Order_Details (id, order_number, product_id, quantity, unit_price)
VALUES 
(1, 1001, 1, 2, 20.00), -- 2 Notebooks
(2, 1001, 2, 1, 5.00),  -- 1 Pen
(3, 1002, 3, 1, 150.00), -- 1 Lamp
(4, 1002, 2, 1, 5.00);   -- 1 Pen


SELECT * FROM Categories;

SELECT * FROM Country;

SELECT * FROM State;

SELECT * FROM City;

SELECT * FROM Area;

SELECT * FROM Address;

SELECT * FROM Supplier;

SELECT * FROM Product;

SELECT * FROM ProductSupplier;

SELECT * FROM Customer;

SELECT * FROM Orders;

SELECT * FROM Order_Details;

