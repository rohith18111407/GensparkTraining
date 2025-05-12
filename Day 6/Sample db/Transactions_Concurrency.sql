CREATE TABLE tbl_bank_accounts
(
account_id SERIAL PRIMARY KEY,
account_name VARCHAR(100),
balance NUMERIC(10,2)
);


INSERT INTO tbl_bank_accounts
(account_name,balance)
VALUES
('Alice',5000.00),
('Bob',3000.00);

SELECT * 
FROM tbl_bank_accounts;


-- Perform Transaction/Tran
BEGIN;

UPDATE tbl_bank_accounts
SET balance=balance-500
WHERE account_name='Alice';

UPDATE tbl_bank_accounts
SET balance=balance+500
WHERE account_name='Bob';

COMMIT;

SELECT * 
FROM tbl_bank_accounts;

-- Introducing Error (Rollback)


BEGIN;

UPDATE tbl_bank_accounts
SET balance=balance-500
WHERE account_name='Alice';

UPDATE tbl_bank_account
SET balance=balance+500
WHERE account_name='Bob';

ROLLBACK;

SELECT * 
FROM tbl_bank_accounts;

-- Savepoints: Partial Rollback
--Example 1

BEGIN;

UPDATE tbl_bank_accounts
SET balance=balance-100
WHERE account_name='Alice';

SAVEPOINT after_alice;

UPDATE tbl_bank_account
SET balance=balance+100
WHERE account_name='Bob';

ROLLBACK TO after_alice;

UPDATE tbl_bank_accounts
SET balance=balance+100
WHERE account_name='Bob';

COMMIT;

SELECT * 
FROM tbl_bank_accounts;

--Example 2

BEGIN;

UPDATE tbl_bank_accounts
SET balance=balance-100
WHERE account_name='Alice';

SAVEPOINT after_alice;

UPDATE tbl_bank_account
SET balance=balance+100
WHERE account_name='Bob';

ROLLBACK TO after_alice;


-- Auto Commit - for Simple DML 
UPDATE tbl_bank_accounts
SET balance=balance+100
WHERE account_name='Bob';

-- Errors -> Rollback or Commit

COMMIT;

SELECT * 
FROM tbl_bank_accounts;

-- Example 3
/*
Stage-1 -> Commited
Stage-2 -> Rollback to some savepoint
*/

-- Raising Notice
ABORT;
SELECT * 
FROM tbl_bank_accounts;


BEGIN;
DO $$
DECLARE current_balance NUMERIC;
BEGIN
	SELECT balance INTO current_balance
	FROM tbl_bank_accounts
	WHERE account_name='Alice';

	IF current_balance >= 4500 THEN
		UPDATE tbl_bank_accounts 
		SET balance = balance - 4500
		WHERE account_name = 'Alice';

		UPDATE tbl_bank_accounts 
		SET balance = balance + 4500
		WHERE account_name = 'Bob';
	ELSE
		RAISE NOTICE 'Insufficient Funds!';
		ROLLBACK;
	END IF;
END;
$$;

ABORT;

SELECT * 
FROM tbl_bank_accounts;


-- Example 4

-- Inside BEGIN; or START TRANSACTION; nothing is auto-committed, the changes are for the current instance but it is not permanaently saved in the database. To save it permanently , run COMMIT;
START TRANSACTION;

UPDATE tbl_bank_accounts
SET balance = balance + 500
WHERE account_name = 'Alice';

SELECT * 
FROM tbl_bank_accounts;

-- At this point, change is not committed yet(for current session, values is updated and changes are not permanent in database)
-- Open a different instance(query tool) and run the select command, you will not receive updated values until you COMMIT;.	

COMMIT;
-- At this point, even when you opena a new instance, you will received the permanently updated value from database.


/*
Concurrency


PostgreSQL handles concurrency using:
1. MVCC (Multi Version Concurrency Control)
MVCC allows multiple versions of the same data (rows) to exist temporarily,
so readers and writers don't block each other.

MVCC is ACID compliant

Examples:

1. 
User A: Reads
User B: Updates

2.
1000 users check balance (reads)
10 users perform withdrawals (writes)


Read Committed
-- Trans A 
BEGIN;
UPDATE products
SET price = 500
WHERE id = 1;

-- Trans B
BEGIN;
SELECT price
FROM products
WHERE id = 1;


Repeatable Read
-- Trans A
BEGIN ISOLATION LEVEL REPEATABLE READ;
SELECT price FROM products
WHERE id = 1; -- 450

-- Trans B
BEGIN;
UPDATE products
SET price = 500 WHERE id = 1;
COMMIT;

-- Trans A
SELECT price FROM products
WHERE id = 1; -- 450
COMMIT;


2. Isolation Levels
	1. READ UNCOMMITTED -> PSQL not supported
	2. READ COMMITTED -> Default
	3. Repeatable Read -> Ensures repeatable reads
		1. Dirty Reads
		2. Phantom Reads
	4. Serializeable -> Full Isolation (Safe but slow, no dirt read, no lost updates, no repeatable read, no phantom read) 

Problems without proper concurrency control:

1. InconsistenT Reads / Dirty Reads: Reading uncommitted data from another transaction, which might later disappear
	
	Transaction A updates a row but doesn't commit yet.
	Transaction B reads the updated row.
	Transaction A rolls back the updated row.
	Now transaction B has read the data that never officially existed - that's a dirty read.

Why Dirty Reads happen:
	They occur in databases running at low isolation levels, particularly:
		Read Uncommitted isolation level -> allow dirty reads.
		Higher isolation levels like Read Committed, Repeatable Read, or Serializeable
		prevent dirty reads but come with performance trade-offs (like more locks or slower concurrency).


2. Lost Update

	Trans A reads a record
	Trans B reads the same record
	Trans A updates and writes it back
	Trans B (still holding the old value) writes back its version, overwriting A's changes.

Solutions to Avoid Lost Updates:

1. Pessimistic Locking (Explicit Locks)
Lock the Record when someone reads it, so no one else can read or write it until the lock is released.
Example: SELECT.... FOR UPDATE in SQL.

Prevents concurrency but can reduce performance due to blocking.

2. Optimistic Locking (Versioning).
Common and scalable solution.
Each record has a version or timestamp.

When Updating, you check that version hasn't changed since you read it.
If it is changed, you reject the update (user must retry).
Example:

UPDATE products
SET price=100, version = version + 1
WHERE id=1 AND version = 3;

3. Serializable Isolation level

In database transactions, using the highest isolation level (SERIALIZEABLE) can prevent lost updates.
But it's heavier and can cause performance issues (due to more locks or transaction retries).


Which solution is Best?
For webApps and APIs: Optimistic locking is often the best balance (fast reads + safe writes).
For critical financial systems: Pessimistic locking may be safer.


Inconsisten Reads / read anomalies:

1. Dirty Read
2. Non-Repeatable Read
	Trans A reads a row -- 100
	Trans B updates and commits the row, then --90
	Trans A reads the row again and sees different data.

*/


-- Concurrency

-- 1. Dirty Read

ABORT;

-- Step1: Set up a sample table

CREATE TABLE Accounts
(
	ID INT PRIMARY KEY,
	balance INT
);

INSERT INTO Accounts
VALUES
(1,1000);

-- Step 2: Trans A

BEGIN TRANSACTION;

UPDATE Accounts
SET balance=0
WHERE id=1;


-- Step 3: Trans B
-- Allow Dirty Read
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
BEGIN TRANSACTION;
SELECT balance
FROM Accounts
WHERE id=1; -- User B reads 0 as balance

-- Step 4: Trans A decides to Rollback
ROLLBACK;
-- balance=1000 for User A but 0 for User B 

-- 2. Lost update

--Trans A
BEGIN;
SELECT balance 
FROM Accounts
WHERE id=1; -- 100
-- Thinks to add 50


--Trans B
BEGIN;
SELECT balance 
FROM Accounts
WHERE id=1; -- 100
-- Thinks to sub 30

UPDATE Accounts
SET balance=70
WHERE id=1;
COMMIT;

--Trans A
UPDATE Accounts
SET balance=150
WHERE id=1;
COMMIT;

-- 2. Non-Repeatable Read

-- Trans A
BEGIN;
SELECT balance
FROM Accounts
WHERE id=1; -- 1000


-- Trans B
UPDATE Accounts
SET balance = balance - 200
WHERE id=1;
COMMIT;

-- Trans A
BEGIN;
SELECT balance
FROM Accounts
WHERE id=1; -- 1000-200=800


-- Phantom Reads
-- Trans A: SELECT * FROM orders WHERE amount > 1000; RETURNS 5 ROWS
-- Another Trans inserts a new order with amount 1200 and commits - now re-running the query returns 6 rows.

-- Trans A
BEGIN;
SELECT *
FROM Accounts
WHERE balance > 500;
-- 1 row

-- Trans B
BEGIN;
INSERT INTO Accounts
(id, balance)
VALUES
(2,600);
COMMIT;

-- Trans A
SELECT *
FROM Accounts
WHERE balance > 500;
-- 2 rows
-- A phantom new row appeared!


-- 1. MVCC (Multi Version Concurrency Control)

-- Example 1: Read while someone is updating

-- Trans A
BEGIN; -- price = 450
UPDATE products
SET price = 500
WHERE id=1;

-- Trans B
BEGIN;
SELECT price
FROM products
WHERE id=1; -- 450 until Trans A commit








