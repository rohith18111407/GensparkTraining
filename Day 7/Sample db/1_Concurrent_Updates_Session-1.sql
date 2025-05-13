-- Tasks
DROP TABLE IF EXISTS accounts;
CREATE TABLE accounts (
    id SERIAL PRIMARY KEY,
    name TEXT,
    balance NUMERIC
);

INSERT INTO accounts (name, balance) VALUES
('Alice', 1000),
('Bob', 1500);

SELECT *
FROM accounts;


-- 1. Try two concurrent updates to same row → see lock in action.

BEGIN;
UPDATE accounts 
SET balance = balance + 100 
WHERE id = 1; -- id = 1 -> 1100

SELECT *
FROM accounts;
-- not COMMIT yet




-- COMMIT;


