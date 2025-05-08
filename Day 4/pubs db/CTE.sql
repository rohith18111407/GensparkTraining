-- CTE = Common Table Expression
-- A CTE is a temporary result set in SQL that you can reference within a SELECT, INSERT, UPDATE, or DELETE statement. 
-- It improves readability and structure, especially when working with complex queries or recursive data.

WITH cteAuthors AS (
    SELECT au_id, CONCAT(au_fname, ' ', au_lname) AS author_name
    FROM authors
)
SELECT * 
FROM cteAuthors;

-- CTE with Pagination

DECLARE @page INT =1, @pageSize INT=10;
WITH ctePaginatedBooks AS( 
	SELECT  title_id,title, price, ROW_Number() OVER (ORDER BY price DESC) AS RowNum
	FROM titles
)
SELECT * 
FROM ctePaginatedBooks 
WHERE rowNum BETWEEN ((@page-1)*(@pageSize+1)) and (@page*@pageSize)


--create a storedPorcedure(sp) that will take the page number and size as param and print the books

CREATE OR ALTER PROC proc_ctePaginatedBooks(@page INT=1, @pageSize INT=10)
AS
BEGIN
	WITH ctePaginatedBooks AS( 
		SELECT  title_id,title, price, ROW_Number() OVER (ORDER BY price DESC) AS RowNum
		FROM titles
	)
	SELECT * 
	FROM ctePaginatedBooks 
	WHERE rowNum BETWEEN ((@page-1)*(@pageSize+1)) and (@page*@pageSize)
END

GO

proc_ctePaginatedBooks 2,5

GO

-- Latest Pagination method
-- offset 10 rows = Skips the first 10 rows of the sorted result (i.e., starts from the 11th row).
-- FETCH NEXT 10 ROWS ONLY = Returns the next 10 rows (i.e., rows 11 to 20 in the sorted list).

SELECT title_id,title,price
FROM titles
ORDER BY price DESC
OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY