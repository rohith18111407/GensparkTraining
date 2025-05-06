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

/* DROP Existing Tables in correct order to avoid FK conflicts */
DROP TABLE IF EXISTS ProductSupplier;
DROP TABLE IF EXISTS PurchaseDetails;
DROP TABLE IF EXISTS Purchase;
DROP TABLE IF EXISTS Product;
DROP TABLE IF EXISTS Supplier;
DROP TABLE IF EXISTS Customer;

-- 1. Product Table
CREATE TABLE Product (
    ProductID CHAR(5) PRIMARY KEY,
    ProductName VARCHAR(100),
    Description TEXT,
    Price DECIMAL(10, 2),
    Stock INT
);

-- 2. Supplier Table
CREATE TABLE Supplier (
    SupplierID CHAR(5) PRIMARY KEY,
    SupplierName VARCHAR(100),
    ContactNumber VARCHAR(15),
    Address TEXT
);

-- 3. ProductSupplier Table (Many-to-Many)
CREATE TABLE ProductSupplier (
    ProductID CHAR(5),
    SupplierID CHAR(5),
    PRIMARY KEY (ProductID, SupplierID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID),
    FOREIGN KEY (SupplierID) REFERENCES Supplier(SupplierID)
);

-- 4. Customer Table
CREATE TABLE Customer (
    CustomerID CHAR(5) PRIMARY KEY,
    Name VARCHAR(100),
    Email VARCHAR(100),
    Phone VARCHAR(15),
    Address TEXT
);

-- 5. Purchase Table (with CustomerID)
CREATE TABLE Purchase (
    PurchaseID CHAR(5) PRIMARY KEY,
    CustomerID CHAR(5),
    PurchaseDate DATE,
    TotalAmount DECIMAL(10, 2),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);

-- 6. PurchaseDetails Table (junction between Purchase and Product)
CREATE TABLE PurchaseDetails (
    PurchaseID CHAR(5),
    ProductID CHAR(5),
    Quantity INT,
    Price DECIMAL(10, 2),
    PRIMARY KEY (PurchaseID, ProductID),
    FOREIGN KEY (PurchaseID) REFERENCES Purchase(PurchaseID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

--  Insert Data in Order

-- Products
INSERT INTO Product (ProductID, ProductName, Description, Price, Stock)
VALUES 
('P1','Pen', 'Blue Ink Pen', 5.00, 100),
('P2','Notebook', '200 pages', 20.00, 50),
('P3','Pencil', 'HB Pencil', 2.00, 200);

-- Suppliers
INSERT INTO Supplier (SupplierID, SupplierName, ContactNumber, Address)
VALUES 
('S1','ABC Supplies', '9876543210', 'New York'),
('S2','XYZ Traders', '9123456780', 'Los Angeles');

-- ProductSupplier Junction Table
INSERT INTO ProductSupplier (ProductID, SupplierID)
VALUES 
('P1', 'S1'), 
('P2', 'S1'), 
('P1', 'S2'), 
('P3', 'S2');

-- Customers
INSERT INTO Customer (CustomerID, Name, Email, Phone, Address)
VALUES 
('C1','Virat Kohli', 'viratk@example.com', '9998877665', 'Boston'),
('C2','Dhoni', 'dhoni@example.com', '8887766554', 'Chicago');

-- Purchase with CustomerID included
INSERT INTO Purchase (PurchaseID, CustomerID, PurchaseDate, TotalAmount)
VALUES ('PU1', 'C1', '2025-05-06', 30.00);

-- Purchase Details (junction between Purchase and Product)
INSERT INTO PurchaseDetails (PurchaseID, ProductID, Quantity, Price)
VALUES 
('PU1', 'P1', 2, 5.00),  -- 2 Pens
('PU1', 'P2', 1, 20.00); -- 1 Notebook




SELECT *
FROM Product;

SELECT * 
FROM Supplier;

SELECT *
FROM ProductSupplier;

SELECT *
FROM Customer;


SELECT *
FROM Purchase;

SELECT *
FROM PurchaseDetails;
