-- 5. Explore about Lock Modes.

-- In PostgreSQL, the locking mechanism ensures data consistency and prevents conflicts between concurrent transactions. 
-- PostgreSQL supports multiple lock modes, categorized into row-level locks and table-level locks. 
-- These locks are part of its MVCC (Multi-Version Concurrency Control) strategy.


/*
 1. Row-Level Lock Modes

 These locks apply to individual rows in a table and are mostly acquired by operations like SELECT FOR UPDATE, UPDATE, DELETE.

 Lock Mode						Description
FOR UPDATE				Acquired when a row is selected for a future UPDATE or DELETE. 
						Blocks other FOR UPDATE, FOR NO KEY UPDATE, UPDATE, DELETE.
FOR NO KEY UPDATE		Weaker than FOR UPDATE. Prevents updates to row data but not primary/unique key values.
FOR SHARE				Allows others to also take FOR SHARE. Prevents DELETE or UPDATE.
FOR KEY SHARE			Weaker than FOR SHARE. Used by SELECT FOR SHARE. Protects against foreign key modifications.

*/

-- Strong lock: row will be locked for updates/deletes
SELECT * FROM accounts WHERE id = 1 FOR UPDATE;

-- Weaker: allows SELECT FOR SHARE from other sessions
SELECT * FROM accounts WHERE id = 1 FOR SHARE;


/*
2. Table-Level Lock Modes

These locks apply to the entire table, not individual rows.


Lock Mode					Acquired By								Blocks
ACCESS SHARE				SELECT									Blocks only ACCESS EXCLUSIVE
ROW SHARE					SELECT FOR UPDATE / SHARE				EXCLUSIVE, ACCESS EXCLUSIVE
ROW EXCLUSIVE				INSERT, UPDATE, DELETE					SHARE, SHARE ROW EXCLUSIVE, etc.
SHARE UPDATE EXCLUSIVE		ANALYZE									SHARE, EXCLUSIVE, etc.
SHARE						LOCK TABLE ... IN SHARE MODE			ROW EXCLUSIVE, etc.
SHARE ROW EXCLUSIVE			Rare									Most other locks
EXCLUSIVE					LOCK TABLE ... IN EXCLUSIVE MODE		Everything except ACCESS SHARE
ACCESS EXCLUSIVE			DROP, TRUNCATE, ALTER					All other locks
*/

-- Locks the entire table in EXCLUSIVE mode
LOCK TABLE accounts IN EXCLUSIVE MODE;



-- Viewing Current Locks

SELECT
    pid,
    locktype,
    mode,
    granted,
    relation::regclass AS table,
    query
FROM pg_locks
JOIN pg_stat_activity USING (pid)
WHERE relation IS NOT NULL;



/*
When Are Locks Used?

Operation					Locks Acquired
SELECT						ACCESS SHARE
SELECT ... FOR UPDATE		ROW EXCLUSIVE
INSERT, UPDATE, DELETE		ROW EXCLUSIVE + row-level lock
ALTER TABLE					ACCESS EXCLUSIVE
DROP TABLE					ACCESS EXCLUSIVE

*/



/*
Notes:

- Locks prevent data corruption in concurrent environments.
- Row-level locks allow high concurrency (MVCC).
- Table-level locks are stronger and block more operations.
- PostgreSQL automatically acquires minimal required locks, but you can manually escalate them using LOCK TABLE.

*/


