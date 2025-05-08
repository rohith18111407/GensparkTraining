select * from products where 
try_cast(json_value(details,'$.spec.cpu') as nvarchar(20)) ='i7'

GO

-- Procedure using OUT parameter

CREATE OR ALTER PROC proc_FilterProducts(@pcpu VARCHAR(20), @pcount INT OUT)
AS
BEGIN
      SET @pcount = (
			SELECT COUNT(*) 
			FROM products 
			WHERE try_cast(json_value(details,'$.spec.cpu') AS NVARCHAR(20)) =@pcpu
			)
END

GO

BEGIN
  DECLARE @cnt INT
  EXEC proc_FilterProducts 'i7', @cnt out
  print concat('The number of computers is ',@cnt)
END

GO

