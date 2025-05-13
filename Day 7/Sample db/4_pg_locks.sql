SELECT
    l.pid,
    l.relation::regclass AS table_name,
    l.mode,
    l.transactionid,
    l.granted,
    a.query
FROM pg_locks l
JOIN pg_stat_activity a ON l.pid = a.pid
WHERE l.relation IS NOT NULL;



/*
l.pid				Process ID of the backend holding the lock.
relation::regclass	Converts the relation OID into a readable table name.
mode				Lock mode (e.g., RowExclusiveLock, AccessShareLock, etc.).
transactionid		Shows if the lock is a transaction lock.
granted				true if the lock has been granted, false if it's waiting.
a.query				The SQL query that the backend is executing.
granted = false 	means it's waiting for the lock.
*/