SELECT *
FROM accounts
WHERE id = 2;

BEGIN;
UPDATE accounts 
SET balance = balance + 100 
WHERE id = 2;
-- Wait
-- Go back to session - 1


UPDATE accounts 
SET balance = balance + 100 
WHERE id = 1;
-- Deadlock will occur! PostgreSQL detects it and aborts one transaction.

