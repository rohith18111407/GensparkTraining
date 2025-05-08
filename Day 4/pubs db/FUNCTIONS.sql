-- SCALAR FUNCTION = Function that returns a single value

CREATE OR ALTER FUNCTION  fn_CalculateTax(@baseprice float, @tax float)
RETURNS FLOAT
AS
BEGIN
     RETURN (@baseprice +(@baseprice*@tax/100))
END

GO

SELECT dbo.fn_CalculateTax(1000,10)

GO

SELECT title Book_Name,price Price_exc_GST,dbo.fn_CalculateTax(price,12) Price_inc_GST 
FROM titles

GO

-- TABLE VALUED FUNCTION = returns a table
-- TABLE VALUED FUNCTION does not require BEGIN ... END

CREATE FUNCTION fn_tableSample(@minprice FLOAT)
RETURNS TABLE
AS
    RETURN (SELECT title,price 
			FROM titles 
			WHERE price>= @minprice
			)

GO

SELECT * 
FROM dbo.fn_tableSample(10)

GO

--older and slower but supports more logic
-- dbo = database owner

CREATE OR ALTER FUNCTION fn_tableSampleOld(@minprice FLOAT)
RETURNS @Result TABLE(Book_Name NVARCHAR(100), price FLOAT)
AS
BEGIN
    INSERT INTO @Result SELECT title,price FROM titles WHERE price>= @minprice
    RETURN 
END

GO

SELECT * 
FROM dbo.fn_tableSampleOld(10)

GO