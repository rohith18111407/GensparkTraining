/*
Locking Mechanisms

PostgreSQL automatically appplies locks, but you can control them manually when needed.

Types of Lock

1. Row Level lock
2. Table Level lock
3. Advisory Lock


MVCC vs Locks

MVCC allows readers and writers to work together without blocking.
Locks are needed when multiple writers try to touch the same row or table.

Simple Rule of Locks

1. Readers don't block each other.
2. Writers block other writers on the same row.


*/

-- 1. Row Level Locking (Default Behavior) / Implicit Lock
-- Two users updating the same row

-- Trans A = session 1
BEGIN;
UPDATE products
SET price=500
WHERE id=1;

-- Trans B = session 2
BEGIN;
UPDATE products
SET price=700
WHERE id=1;

-- B can only update after Trans A commit or rollback. B waits until A complete or rollback. 
-- session 1 will be having lock in the database hence sessi	on2 will show waiting for the query to complete.
-- If two writers are updating two different rows, then the update works.
-- Locking is only when two writers are trying to update the same row.


-- 2. Table Level Locks / Explicit Table Locks

 1. ACCESS SHARE = SELECT
 -- Allows reads and writes.

BEGIN;
LOCK TABLE products
IN ACCESS SHARE MODE;
-- Allows other SELECTs, even INSERT/DELETE at the same time.


 2. ROW SHARE 
-- SELECT ... FOR UPDATE -> lock the selected row for later update

BEGIN;
LOCK TABLE products
IN ROW SHARE MODE;
-- SELECT ... FOR UPDATE, reads are allowed, conflicting row locks are blocked, writes allowed.


3. EXCLUSIVE 
-- Blocks writes (INSERT, UPDATE, DELETE) but allows reads (SELECT)

BEGIN;
LOCK TABLE products
IN EXCLUSIVE MODE;


4. ACCESS EXCLUSIVE
-- Blocks everything.
-- Most aggressive lock.
-- used by ALTER TABLE, DROP TABLE, TRUNCATE
-- Internally used by DDL.

-- A
BEGIN;
LOCK TABLE products
IN ACCESS EXCLUSIVE MODE;
-- Table is now fully locked!

-- B
SELECT *
FROM products;
-- B will wait until A commits or rollback.



-- Explicit Row Locks -> SELECT ... FOR UPDATE

--A
BEGIN;
SELECT *
FROM products
WHERE id = 1
FOR UPDATE;
-- Row id = 1 is now locked

-- B
BEGIN;
UPDATE products
SET price = 700
WHERE id = 1;
-- B is blocked until A finishes.

-- SELECT .. FOR UPDATE locks the row early so that no one can change it midway.
-- Usecases: Banking, Ticket booking systen, Inventory management systems.


/*
DeadLock

A deadlock happens when 
	- Trans A waits for B
	- Trans B waits for A
	- They both wait forever
*/

-- Trans A
BEGIN;
UPDATE products
SET price = 500
WHERE id = 1;
-- A locks id = 1 row 

-- Trans B
BEGIN;
UPDATE products
SET price = 600
WHERE id = 2;
-- B locks id = 2 row

-- Trans A
UPDATE products
SET price = 500
WHERE id = 2;
-- A locks id = 2 row but it is already locked by B

-- Trans B
UPDATE products 
SET price = 600
WHERE id = 1;
-- B locks id = 1 row but it is already locked by A
-- PSQL detects a deadlock
-- ERROR: deadlock detected.
-- It automatically aborts a transaction to resolve the deadlock.



-- 3. Advisory Locks / Custom Locks

SELECT pg_advisory_lock(12345);

-- critical ops

-- Release the lock
SELECT pg_advisory_unlock(12345);




