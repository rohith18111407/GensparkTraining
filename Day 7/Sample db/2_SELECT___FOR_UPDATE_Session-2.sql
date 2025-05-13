BEGIN;
UPDATE accounts 
SET balance = balance + 300 
WHERE id = 2;
-- This will wait until Session 1 releases the lock

-- SELECT ... FOR UPDATE locks the selected rows as if they were being updated.
-- Prevents other transactions from updating/deleting them until the lock is released.


