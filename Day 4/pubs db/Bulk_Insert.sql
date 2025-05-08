--Bulk insert from data.csv after creation of people tabel


sp_help authors

GO

TRUNCATE TABLE people;

DROP PROC proc_BulkInsertPosts;

CREATE TABLE people
(
	id INT PRIMARY KEY,
	name NVARCHAR(20),
	age INT
);

GO

-- FIRSTROW = 2 mean start importing data from the second row of the CSV file — this is typically used to skip the header row

CREATE OR ALTER PROC proc_BulkInsert(@filepath NVARCHAR(500))
AS
BEGIN
   DECLARE @insertQuery NVARCHAR(MAX)

   SET @insertQuery = '
		BULK INSERT people 
		FROM '''+ @filepath +'''
		WITH(
		FIRSTROW =2,
		FIELDTERMINATOR='','',
		ROWTERMINATOR = ''\n'')
		'
   EXEC sp_executesql @insertQuery
END

GO

proc_BulkInsert 'C:\GenSpark Training\Day 4\pubs db\Data.csv'

GO

SELECT *
FROM people

GO

-- Use of Bulk Insert log

CREATE TABLE BulkInsertLog
(
	LogId INT IDENTITY(1,1) PRIMARY KEY,
	FilePath NVARCHAR(1000),
	status NVARCHAR(50) CONSTRAINT chk_status CHECK(status in('Success','Failed')),
	Message NVARCHAR(1000),
	InsertedOn DATETIME DEFAULT GetDate()
);

GO

DROP PROC proc_BulkInsert;

GO

TRUNCATE TABLE people;

GO

SELECT *
FROM people;

GO

CREATE or ALTER PROC proc_BulkInsert(@filepath NVARCHAR(500))
AS
BEGIN
  BEGIN TRY
	   DECLARE @insertQuery NVARCHAR(MAX)

	   SET @insertQuery = '
		BULK INSERT people 
		FROM '''+ @filepath +'''
		WITH(
		FIRSTROW =2,
		FIELDTERMINATOR='','',
		ROWTERMINATOR = ''\n'')
		'

		EXEC sp_executesql @insertQuery

		INSERT INTO BulkInsertLog(filepath,status,message)
		VALUES(@filepath,'Success','Bulk insert completed')
  END TRY
  BEGIN CATCH
		 INSERT INTO BulkInsertLog(filepath,status,message)
		 VALUES(@filepath,'Failed',Error_Message())
  END CATCH 
END

GO

proc_BulkInsert 'C:\GenSpark Training\Day 4\pubs db\Data.csv'

GO

SELECT * 
FROM BulkInsertLog;

SELECT *
FROM people;

