-- Cursors 
--1. Write a cursor to list all customers and how many rentals each made. Insert these into a summary table.

DROP TABLE IF EXISTS customer_rental_summary;


CREATE TABLE customer_rental_summary (
    customer_id INT,
    rental_count INT
);


DO $$
DECLARE
    c RECORD;
	customer_rental_cursor CURSOR FOR
		SELECT customer_id, COUNT(*) AS rental_count
        FROM rental
        GROUP BY customer_id;
BEGIN
	OPEN customer_rental_cursor;

	LOOP
		FETCH customer_rental_cursor INTO c;
		EXIT WHEN NOT FOUND;
        INSERT INTO customer_rental_summary VALUES (c.customer_id, c.rental_count);
    END LOOP;

	CLOSE customer_rental_cursor;
END;
$$;


SELECT *
FROM customer_rental_summary;


 
--2. Using a cursor, print the titles of films in the 'Comedy' category rented more than 10 times.


DO $$
DECLARE
    film_rec RECORD;
	cur CURSOR FOR
		SELECT f.title, COUNT(*) AS rentals
        FROM film f
        JOIN film_category fc ON f.film_id = fc.film_id
        JOIN category c ON c.category_id = fc.category_id
        JOIN inventory i ON i.film_id = f.film_id
        JOIN rental r ON r.inventory_id = i.inventory_id
        WHERE c.name = 'Comedy'
        GROUP BY f.title
        HAVING COUNT(*) > 10;
		
BEGIN
	OPEN cur;

	LOOP 
		FETCH cur INTO film_rec;
		EXIT WHEN NOT FOUND;
        RAISE NOTICE 'Title: %, Rentals: %', film_rec.title, film_rec.rentals;
    END LOOP;
	CLOSE cur;
END;
$$;



 
--3. Create a cursor to go through each store and count the number of distinct films available, and insert results into a report table.

DROP TABLE IF EXISTS store_film_count;

CREATE TABLE store_film_count (
    store_id INT,
    film_count INT
);


DO $$
DECLARE
    s RECORD;
	cur CURSOR FOR
		SELECT store_id 
		FROM store;
BEGIN
	OPEN cur;

	LOOP
		FETCH cur INTO s;
		EXIT WHEN NOT FOUND;

        INSERT INTO store_film_count
	        SELECT s.store_id, COUNT(DISTINCT i.film_id)
	        FROM inventory i
	        WHERE i.store_id = s.store_id;
    END LOOP;
END;
$$;

SELECT *
FROM store_film_count;


 
-- 4. Loop through all customers who haven't rented in the last 6 months and insert their details into an inactive_customers table.

DROP TABLE IF EXISTS inactive_customers;

CREATE TABLE inactive_customers (
    customer_id INT,
    first_name TEXT,
    last_name TEXT,
    email TEXT
);


DO $$
DECLARE
    c_rec RECORD;
	cur CURSOR FOR
		SELECT c.customer_id, c.first_name, c.last_name, c.email
        FROM customer c
        WHERE NOT EXISTS (
            SELECT 1 FROM rental r
            WHERE r.customer_id = c.customer_id
              AND r.rental_date > CURRENT_DATE - INTERVAL '6 months'
        );
BEGIN
	OPEN cur;

	LOOP
		FETCH cur INTO c_rec;
		EXIT WHEN NOT FOUND;
	
        INSERT INTO inactive_customers VALUES (c_rec.customer_id, c_rec.first_name, c_rec.last_name, c_rec.email);
    END LOOP;
	CLOSE cur;
END;
$$;

SELECT *
FROM inactive_customers;


--------------------------------------------------------------------------
 
-- Transactions 
--1. Write a transaction that inserts a new customer, adds their rental, and logs the payment – all atomically.
ABORT;

DO $$
DECLARE
    v_customer_id INT;
    v_rental_id INT;
BEGIN
    -- Insert new customer and capture ID
    INSERT INTO customer (store_id, first_name, last_name, email, address_id, active, create_date)
    VALUES (1, 'Alice', 'Cooper', 'alice.cooper@example.com', 1, 1, NOW())
    RETURNING customer_id INTO v_customer_id;

    -- Insert rental for the new customer and capture rental ID
    INSERT INTO rental (rental_date, inventory_id, customer_id, staff_id)
    VALUES (NOW(), 1, v_customer_id, 1)
    RETURNING rental_id INTO v_rental_id;

    -- Insert payment using captured IDs
    INSERT INTO payment (customer_id, staff_id, rental_id, amount, payment_date)
    VALUES (v_customer_id, 1, v_rental_id, 4.99, NOW());
END;
$$;



-- 2. Simulate a transaction where one update fails (e.g., invalid rental ID), and ensure the entire transaction rolls back.

SELECT payment_id
FROM payment
ORDER BY payment_id;

SELECT customer_id
FROM payment
ORDER BY customer_id;

SELECT DISTINCT staff_id
FROM payment
ORDER BY staff_id;

BEGIN;

-- Valid update
UPDATE payment SET amount = amount + 1 WHERE payment_id = 17503;

-- Invalid (assume rental_id 999999 does not exist, FK fails)
INSERT INTO payment (customer_id, staff_id, rental_id, amount, payment_date)
VALUES (2, 1, 999999, 10.00, NOW());

-- This will fail and block next line
COMMIT;

-- Result: entire transaction rolled back

ABORT;

 
-- 3. Use SAVEPOINT to update multiple payment amounts. Roll back only one payment update using ROLLBACK TO SAVEPOINT.

SELECT payment_id
FROM payment
ORDER BY payment_id;

SELECT *
FROM payment
WHERE payment_id = 17503;


SELECT *
FROM payment
WHERE payment_id = 17504;


BEGIN;
SAVEPOINT sp1;

UPDATE payment SET amount = amount + 10 WHERE payment_id = 17503;
SAVEPOINT sp2;

UPDATE payment SET amount = amount + 20 WHERE payment_id = 17504;

-- Something went wrong
ROLLBACK TO sp2;

-- payment_id = 17503 updated
-- payment_id = 17504 unchanged

COMMIT;

SELECT *
FROM payment
WHERE payment_id = 17503;


SELECT *
FROM payment
WHERE payment_id = 17504;


 
-- 4. Perform a transaction that transfers inventory from one store to another (delete + insert) safely.

SELECT DISTINCT inventory_id
FROM inventory
WHERE store_id = 1
ORDER BY inventory_id;

SELECT DISTINCT inventory_id
FROM inventory
WHERE store_id = 2
ORDER BY inventory_id;

ABORT;

BEGIN;
 
UPDATE inventory
SET store_id = 2
WHERE store_id = 1 AND film_id = 100;
 
COMMIT;





 
-- 5. Create a transaction that deletes a customer and all associated records (rental, payment), ensuring referential integrity.

SELECT DISTINCT customer_id
FROM customer
ORDER BY customer_id;

BEGIN;

-- Assume customer_id = 2
DELETE FROM payment WHERE customer_id = 2;
DELETE FROM rental WHERE customer_id = 2;
DELETE FROM customer WHERE customer_id = 2;

COMMIT;


SELECT DISTINCT customer_id
FROM customer
ORDER BY customer_id;


----------------------------------------------------------------------------
 
-- Triggers
-- 1. Create a trigger to prevent inserting payments of zero or negative amount.

CREATE OR REPLACE FUNCTION prevent_invalid_payment()
RETURNS TRIGGER AS $$
BEGIN
    IF NEW.amount <= 0 THEN
        RAISE EXCEPTION 'Invalid payment amount';
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE TRIGGER trg_prevent_invalid_payment
BEFORE INSERT ON payment
FOR EACH ROW
EXECUTE FUNCTION prevent_invalid_payment();


-- This should raise an error:
INSERT INTO payment (customer_id, staff_id, rental_id, amount, payment_date)
VALUES (3, 1, 1, -5.00, NOW());




 
--2. Set up a trigger that automatically updates last_update on the film table when the title or rental rate is changed.


CREATE OR REPLACE FUNCTION update_film_timestamp()
RETURNS TRIGGER AS $$
BEGIN
    NEW.last_update := NOW();
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;


CREATE TRIGGER trg_update_film_timestamp
BEFORE UPDATE OF title, rental_rate ON film
FOR EACH ROW
EXECUTE FUNCTION update_film_timestamp();


-- Before update
SELECT title, rental_rate, last_update FROM film WHERE film_id = 1;

-- Trigger will update last_update
UPDATE film SET title = 'New Title' WHERE film_id = 1;

-- After update
SELECT title, rental_rate, last_update FROM film WHERE film_id = 1;


 
--3. Write a trigger that inserts a log into rental_log whenever a film is rented more than 3 times in a week.

DROP TABLE IF EXISTS rental_log;

CREATE TABLE rental_log (
    log_id SERIAL PRIMARY KEY,
    film_id INT NOT NULL,
    log_time TIMESTAMP NOT NULL DEFAULT NOW(),
    message TEXT
);


CREATE OR REPLACE FUNCTION log_frequent_rentals()
RETURNS TRIGGER AS $$
DECLARE
    v_film_id INT;
    rental_count INT;
BEGIN
    -- Get the film ID from the inventory ID of the newly inserted rental
    SELECT i.film_id INTO v_film_id
    FROM inventory i
    WHERE i.inventory_id = NEW.inventory_id;

    -- Count how many times this film has been rented in the last 7 days
    SELECT COUNT(*) INTO rental_count
    FROM rental r
    JOIN inventory i ON r.inventory_id = i.inventory_id
    WHERE i.film_id = v_film_id
      AND r.rental_date >= NOW() - INTERVAL '7 days';

    -- If more than 3 rentals in a week, log it
    IF rental_count > 3 THEN
        INSERT INTO rental_log (film_id, log_time, message)
        VALUES (v_film_id, NOW(), 'Film rented more than 3 times this week');
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

DROP TRIGGER IF EXISTS trg_log_frequent_rentals ON rental;

CREATE TRIGGER trg_log_frequent_rentals
AFTER INSERT ON rental
FOR EACH ROW
EXECUTE FUNCTION log_frequent_rentals();


SELECT *
FROM rental
ORDER BY inventory_id;

SELECT tgname AS trigger_name
FROM pg_trigger
WHERE tgrelid = 'rental'::regclass
  AND NOT tgisinternal;


ALTER TABLE rental DISABLE TRIGGER "trg_block_high_debt";

-- Make sure inventory_id 3 exists and maps to a film
-- Ensure rental times are different
INSERT INTO rental (rental_date, inventory_id, customer_id, staff_id)
VALUES (NOW(), 3, 3, 1);

INSERT INTO rental (rental_date, inventory_id, customer_id, staff_id)
VALUES (NOW() + INTERVAL '1 second', 3, 3, 1);

INSERT INTO rental (rental_date, inventory_id, customer_id, staff_id)
VALUES (NOW() + INTERVAL '2 second', 3, 3, 1);

INSERT INTO rental (rental_date, inventory_id, customer_id, staff_id)
VALUES (NOW() + INTERVAL '3 second', 3, 3, 1);


SELECT * 
FROM rental_log 
ORDER BY log_time DESC;



