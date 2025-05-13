SELECT *
FROM accounts; -- id = 1 -> 1000

BEGIN;
UPDATE accounts 
SET balance = balance + 200 
WHERE id = 1;
-- This will hang (wait for lock) until Session 1 COMMITs or ROLLBACKs


-- Session 1 locks row id=1.
-- Session 2 tries to update the same row → must wait (row-level lock).
-- Once Session 1 COMMITs, Session 2 proceeds.
