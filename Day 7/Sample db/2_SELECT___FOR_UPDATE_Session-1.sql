-- 2. Write a query using SELECT...FOR UPDATE and check how it locks row.

BEGIN;
SELECT * FROM accounts 
WHERE id = 2 FOR UPDATE;
-- Row 2 is now locked
-- Hold the transaction open
