
-- 1) List all orders with the customer name and the employee who handled the order.

SELECT 
	o.OrderId, 
	c.CompanyName CustomerName, 
	CONCAT(e.FirstName,' ',e.LastName) EmployeeName
FROM Orders o
JOIN Employees e ON o.EmployeeID=e.EmployeeID
JOIN Customers c ON o.CustomerID=c.CustomerID;


-- 2) Get a list of products along with their category and supplier name.

SELECT 
    p.ProductName,
    c.CategoryName,
    s.CompanyName AS SupplierName
FROM Products p
JOIN Categories c ON p.CategoryID = c.CategoryID
JOIN Suppliers s ON p.SupplierID = s.SupplierID;


-- 3) Show all orders and the products included in each order with quantity and unit price.

SELECT 
    o.OrderID,
    p.ProductName,
    od.Quantity,
    od.UnitPrice
FROM [Order Details] od
JOIN Orders o ON od.OrderID = o.OrderID
JOIN Products p ON od.ProductID = p.ProductID;


-- 4) List employees who report to other employees (manager-subordinate relationship).

SELECT 
    e1.EmployeeID,
    e1.FirstName + ' ' + e1.LastName AS EmployeeName,
    e2.FirstName + ' ' + e2.LastName AS ManagerName
FROM Employees e1
JOIN Employees e2 ON e1.ReportsTo = e2.EmployeeID;


-- 5) Display each customer and their total order count.

SELECT 
    c.CustomerID,
    c.CompanyName,
    COUNT(o.OrderID) AS OrderCount
FROM Customers c
LEFT JOIN Orders o ON c.CustomerID = o.CustomerID
GROUP BY c.CustomerID, c.CompanyName;


-- 6) Find the average unit price of products per category.

SELECT 
    c.CategoryName,
    AVG(p.UnitPrice) AS AvgUnitPrice
FROM Products p
JOIN Categories c ON p.CategoryID = c.CategoryID
GROUP BY c.CategoryName;


-- 7) List customers where the contact title starts with 'Owner'.

SELECT *
FROM Customers
WHERE ContactTitle LIKE 'Owner%';


-- 8) Show the top 5 most expensive products.

SELECT TOP 5 *
FROM Products
ORDER BY UnitPrice DESC;


-- 9) Return the total sales amount (quantity × unit price) per order.

SELECT 
    od.OrderID,
    SUM(od.Quantity * od.UnitPrice) AS TotalSales
FROM [Order Details] od
GROUP BY od.OrderID;


-- 10) Create a stored procedure that returns all orders for a given customer ID.
-- Input: @CustomerID
CREATE OR ALTER PROCEDURE proc_GetOrdersByCustomer(@CustomerID NVARCHAR(5))
AS
BEGIN
    SELECT *
    FROM Orders
    WHERE CustomerID = @CustomerID;
END;

GO

proc_GetOrdersByCustomer 'LONEP'

GO


-- 11) Write a stored procedure that inserts a new product.
-- Inputs: ProductName, SupplierID, CategoryID, UnitPrice, etc.

CREATE OR ALTER PROCEDURE proc_InsertNewProduct(
    @ProductName NVARCHAR(40),
    @SupplierID INT,
    @CategoryID INT,
    @UnitPrice MONEY,
    @UnitsInStock SMALLINT = NULL,
    @UnitsOnOrder SMALLINT = NULL,
    @ReorderLevel SMALLINT = NULL,
    @Discontinued BIT = 0)
AS
BEGIN
    INSERT INTO Products (ProductName, SupplierID, CategoryID, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
    VALUES (@ProductName, @SupplierID, @CategoryID, @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, @Discontinued);
END;

GO

EXEC proc_InsertNewProduct
    @ProductName = 'Organic Honey',
    @SupplierID = 3,
    @CategoryID = 4,
    @UnitPrice = 15.00;



-- 12) Create a stored procedure that returns total sales per employee.

CREATE OR ALTER PROCEDURE proc_GetSalesByEmployee
AS
BEGIN
    SELECT 
        e.EmployeeID,
        e.FirstName + ' ' + e.LastName AS EmployeeName,
        SUM(od.Quantity * od.UnitPrice) AS TotalSales
    FROM Employees e
    JOIN Orders o ON e.EmployeeID = o.EmployeeID
    JOIN [Order Details] od ON o.OrderID = od.OrderID
    GROUP BY e.EmployeeID, e.FirstName, e.LastName;
END;

GO

EXEC proc_GetSalesByEmployee

-- 13) Use a CTE to rank products by unit price within each category.

WITH cte_ProductRank AS (
    SELECT 
        ProductID,
        ProductName,
        CategoryID,
        UnitPrice,
        RANK() OVER (PARTITION BY CategoryID ORDER BY UnitPrice DESC) AS PriceRank
    FROM Products
)
SELECT * FROM cte_ProductRank;


-- 14) Create a CTE to calculate total revenue per product and filter products with revenue > 10,000.

WITH cte_ProductRevenue AS (
    SELECT 
        p.ProductID,
        p.ProductName,
        SUM(od.Quantity * od.UnitPrice) AS TotalRevenue
    FROM Products p
    JOIN [Order Details] od ON p.ProductID = od.ProductID
    GROUP BY p.ProductID, p.ProductName
)
SELECT *
FROM cte_ProductRevenue
WHERE TotalRevenue > 10000;


-- 15) Use a CTE with recursion to display employee hierarchy.

WITH cte_EmployeeHierarchy AS (
    SELECT 
        EmployeeID,
        FirstName + ' ' + LastName AS EmployeeName,
        ReportsTo,
        0 AS Level
    FROM Employees 
    WHERE ReportsTo IS NULL

    UNION ALL

    SELECT 
        e.EmployeeID,
        e.FirstName + ' ' + e.LastName,
        e.ReportsTo,
        eh.Level + 1
    FROM Employees e
    INNER JOIN cte_EmployeeHierarchy eh ON e.ReportsTo = eh.EmployeeID
)
SELECT *
FROM cte_EmployeeHierarchy
ORDER BY Level, EmployeeName;
