-- Procedure

CREATE OR ALTER PROCEDURE proc_FirstProcedure
AS
BEGIN
	print 'Hello World'
END

GO

EXEC proc_FirstProcedure

GO

-- NVARCHAR uses Unicode, allowing it to store characters from multiple languages and scripts. VARCHAR stores each character using a single byte, NVARCHAR columns typically consume twice as much storage space as equivalent VARCHAR column
DROP TABLE IF EXISTS Products;

CREATE TABLE Products
(
id INT IDENTITY(1,1) CONSTRAINT pk_productId PRIMARY KEY,
name NVARCHAR(100) NOT NULL,
details NVARCHAR(MAX)
);

GO

CREATE OR ALTER PROC proc_InsertProduct(@pname NVARCHAR(100),@pdetails NVARCHAR(max))
AS
BEGIN
    INSERT INTO Products(name,details) VALUES(@pname,@pdetails)
END

GO

proc_InsertProduct 'Laptop','{"brand":"Dell","spec":{"ram":"16GB","cpu":"i5"}}'

GO

proc_InsertProduct 'Laptop','{"brand":"HP","spec":{"ram":"16GB","cpu":"i7"}}'

GO

SELECT * 
FROM Products;

GO

-- Retrieve JSON data (ad-hoc query)

SELECT JSON_QUERY(details,'$.spec') Product_Specification
FROM Products;

GO

-- Procedure to update the product RAM specification

CREATE OR ALTER PROC proc_UpdateProductSpec(@pid int,@newvalue varchar(20))
AS 
BEGIN
	UPDATE PRODUCTS
	SET details=JSON_MODIFY(details,'$.spec.ram',@newvalue)
	WHERE id=@pid;
END

GO

proc_UpdateProductSpec 1,'24GB'

GO

SELECT JSON_QUERY(details,'$.spec') Product_Specification
FROM Products;

GO

-- Scalar Extraction

SELECT id, name, JSON_VALUE(details,'$.brand') Brand_name
FROM Products;

GO

-- Posts Table

DROP TABLE IF EXISTS Posts;

CREATE TABLE Posts
(
	id INT PRIMARY KEY,
	title NVARCHAR(100),
	user_id INT,
	body NVARCHAR(MAX)
);

Go

-- Perforiming Bulk Inserts from JsonData

CREATE OR ALTER PROC proc_BulkInsertPosts(@jsondata NVARCHAR(max))
AS
BEGIN
	INSERT INTO Posts(user_id,id,title,body)
	SELECT userId,id,title,body 
	FROM OPENJSON(@jsondata)
	WITH (userId INT,id INT, title VARCHAR(100), body VARCHAR(max))
END

GO

proc_BulkInsertPosts '[
  {
    "userId": 1,
    "id": 1,
    "title": "sunt aut facere repellat provident occaecati excepturi optio reprehenderit",
    "body": "quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto"
  },
  {
    "userId": 1,
    "id": 2,
    "title": "qui est esse",
    "body": "est rerum tempore vitae\nsequi sint nihil reprehenderit dolor beatae ea dolores neque\nfugiat blanditiis voluptate porro vel nihil molestiae ut reiciendis\nqui aperiam non debitis possimus qui neque nisi nulla"
  }
]'

GO

SELECT *
FROM Posts;

GO

-- Use of TRY_CAST()

SELECT * 
FROM products 
WHERE TRY_CAST(JSON_VALUE(details,'$.spec.cpu') AS NVARCHAR(20)) ='i7';



-- create a procedure that brings post by taking the user_id as parameter

CREATE OR ALTER PROCEDURE proc_GetPostsByUserId(@p_user_id INT)
AS
BEGIN
    SELECT *
    FROM Posts
    WHERE user_id = @p_user_id;
END

GO

proc_GetPostsByUserId 1

GO