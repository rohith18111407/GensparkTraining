-- 3. Intentionally create a deadlock and observe PostgreSQL cancel one transaction.

SELECT *
FROM accounts
WHERE id = 1;

BEGIN;
UPDATE accounts 
SET balance = balance + 100 
WHERE id = 1;
-- Now wait, don’t commit yet
-- Go to Session - 2


-- After Session - 2

UPDATE accounts 
SET balance = balance + 100 
WHERE id = 2;
-- This will now wait on Session 2
-- Go to Session - 2
