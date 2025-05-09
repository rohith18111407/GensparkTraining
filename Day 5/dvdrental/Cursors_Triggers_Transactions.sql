-- Cursor-Based Questions (5)
-- 1. Write a cursor that loops through all films and prints titles longer than 120 minutes.

DO $$
DECLARE
    film_rec RECORD;
BEGIN
    FOR film_rec IN 
        SELECT title, length FROM film WHERE length > 120
    LOOP
        RAISE NOTICE 'Title: %, Length: %', film_rec.title, film_rec.length;
    END LOOP;
END $$;


-- 2. Create a cursor that iterates through all customers and counts how many rentals each made.

DO $$
DECLARE
    cust RECORD;
    rental_count INT;
BEGIN
    FOR cust IN SELECT customer_id, first_name, last_name FROM customer
    LOOP
        SELECT COUNT(*) INTO rental_count FROM rental WHERE customer_id = cust.customer_id;
        RAISE NOTICE 'Customer: % %, Rentals: %', cust.first_name, cust.last_name, rental_count;
    END LOOP;
END $$;


-- 3. Using a cursor, update rental rates: Increase rental rate by $1 for films with less than 5 rentals.

SELECT f.film_id, f.rental_rate 
FROM film f
JOIN inventory i ON f.film_id = i.film_id
LEFT JOIN rental r ON i.inventory_id = r.inventory_id
GROUP BY f.film_id
HAVING COUNT(r.rental_id) < 5

DO $$
DECLARE
    film_rec RECORD;
BEGIN
    FOR film_rec IN 
        SELECT f.film_id FROM film f
        JOIN inventory i ON f.film_id = i.film_id
        LEFT JOIN rental r ON i.inventory_id = r.inventory_id
        GROUP BY f.film_id
        HAVING COUNT(r.rental_id) < 5
    LOOP
        UPDATE film SET rental_rate = rental_rate + 1 WHERE film_id = film_rec.film_id;
    END LOOP;
END $$;

SELECT f.film_id, f.rental_rate 
FROM film f
JOIN inventory i ON f.film_id = i.film_id
LEFT JOIN rental r ON i.inventory_id = r.inventory_id
GROUP BY f.film_id
HAVING COUNT(r.rental_id) < 5


-- 4. Create a function using a cursor that collects titles of all films from a particular category.

CREATE OR REPLACE FUNCTION get_titles_by_category(cat_name TEXT)
RETURNS TEXT AS $$
DECLARE
    title_list TEXT := '';
    film_rec RECORD;
BEGIN
    FOR film_rec IN
        SELECT f.title FROM film f
        JOIN film_category fc ON f.film_id = fc.film_id
        JOIN category c ON fc.category_id = c.category_id
        WHERE c.name = cat_name
    LOOP
        title_list := title_list || film_rec.title || ', ';
    END LOOP;

    RETURN TRIM(BOTH ', ' FROM title_list);
END;
$$ LANGUAGE plpgsql;


SELECT get_titles_by_category('Comedy');


-- 5. Loop through all stores and count how many distinct films are available in each store using a cursor.

DO $$
DECLARE
    store_rec RECORD;
    film_count INT;
BEGIN
    FOR store_rec IN SELECT store_id FROM store
    LOOP
        SELECT COUNT(DISTINCT inventory.film_id)
        INTO film_count
        FROM inventory
        WHERE store_id = store_rec.store_id;

        RAISE NOTICE 'Store ID: %, Distinct Films: %', store_rec.store_id, film_count;
    END LOOP;
END $$;



-- Trigger-Based Questions (5)
-- 6. Write a trigger that logs whenever a new customer is inserted.

CREATE TABLE customer_log (
    log_id SERIAL PRIMARY KEY,
    customer_id INT,
    action_time TIMESTAMP DEFAULT now()
);

CREATE OR REPLACE FUNCTION log_new_customer()
RETURNS TRIGGER AS $$
BEGIN
    INSERT INTO customer_log(customer_id) VALUES (NEW.customer_id);
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_log_customer
AFTER INSERT ON customer
FOR EACH ROW
EXECUTE FUNCTION log_new_customer();


INSERT INTO customer (store_id, first_name, last_name, email, address_id, active)
VALUES (1, 'MS', 'Dhoni', 'msd@gmail.com', 1, 1);


SELECT * FROM customer_log ORDER BY action_time DESC LIMIT 5;



-- 7. Create a trigger that prevents inserting a payment of amount 0.

CREATE OR REPLACE FUNCTION prevent_zero_payment()
RETURNS TRIGGER AS $$
BEGIN
    IF NEW.amount = 0 THEN
        RAISE EXCEPTION 'Payment amount cannot be 0';
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_no_zero_payment
BEFORE INSERT ON payment
FOR EACH ROW
EXECUTE FUNCTION prevent_zero_payment();


-- This should fail due to the trigger:
INSERT INTO payment (customer_id, staff_id, rental_id, amount, payment_date)
VALUES (1, 1, 1, 0, NOW());


-- 8.Set up a trigger to automatically set last_update on the film table before update.

CREATE OR REPLACE FUNCTION update_last_update()
RETURNS TRIGGER AS $$
BEGIN
    NEW.last_update := now();
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_update_last_update
BEFORE UPDATE ON film
FOR EACH ROW
EXECUTE FUNCTION update_last_update();

--  original last_update timestamp for a film
SELECT film_id, title, last_update
FROM film
WHERE film_id = 1;

UPDATE film
SET description = 'Updated description for testing trigger'
WHERE film_id = 1;

SELECT film_id, title, last_update
FROM film
WHERE film_id = 1;



-- 9. Create a trigger to log changes in the inventory table (insert/delete).

CREATE TABLE inventory_audit (
    audit_id SERIAL PRIMARY KEY,
    inventory_id INT,
    film_id INT,
    store_id INT,
    operation TEXT,
    change_time TIMESTAMP DEFAULT now()
);

CREATE OR REPLACE FUNCTION log_inventory_changes()
RETURNS TRIGGER AS $$
BEGIN
    IF TG_OP = 'INSERT' THEN
        INSERT INTO inventory_audit(inventory_id, film_id, store_id, operation)
        VALUES (NEW.inventory_id, NEW.film_id, NEW.store_id, 'INSERT');
        RETURN NEW;
    ELSIF TG_OP = 'DELETE' THEN
        INSERT INTO inventory_audit(inventory_id, film_id, store_id, operation)
        VALUES (OLD.inventory_id, OLD.film_id, OLD.store_id, 'DELETE');
        RETURN OLD;
    END IF;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_inventory_log
AFTER INSERT OR DELETE ON inventory
FOR EACH ROW
EXECUTE FUNCTION log_inventory_changes();

-- insert a new row into inventory
INSERT INTO inventory (film_id, store_id)
VALUES (1, 1);

-- View the audit log
SELECT * FROM inventory_audit
ORDER BY change_time DESC
LIMIT 5;

-- Delete that inventory row
DELETE FROM inventory
WHERE inventory_id = 4582;

--  View the audit log again
SELECT * FROM inventory_audit
WHERE inventory_id = 4582
ORDER BY change_time;


-- 10. Write a trigger that ensures a rental can’t be made for a customer who owes more than $50.

CREATE OR REPLACE FUNCTION block_rental_if_owe()
RETURNS TRIGGER AS $$
DECLARE
    total_due NUMERIC;
BEGIN
    SELECT SUM(amount) INTO total_due FROM payment
    WHERE customer_id = NEW.customer_id;

    IF total_due < 0 OR total_due IS NULL THEN
        total_due := 0;
    END IF;

    IF total_due > 50 THEN
        RAISE EXCEPTION 'Customer owes more than $50';
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_block_high_debt
BEFORE INSERT ON rental
FOR EACH ROW
EXECUTE FUNCTION block_rental_if_owe();

SELECT rental_id FROM rental WHERE customer_id = 1 LIMIT 1;


-- customer who owes more than $50
-- Create debt for customer_id = 1
INSERT INTO payment (customer_id, staff_id, rental_id, amount, payment_date)
VALUES (1, 1, 76, -60.00, now());

-- Attempt to insert rental
INSERT INTO rental (rental_date, inventory_id, customer_id, return_date, staff_id)
VALUES (now(), 1, 1, NULL, 1);




-- Transaction-Based Questions (5)
-- 11. Write a transaction that inserts a customer and an initial rental in one atomic operation.

BEGIN;

  INSERT INTO customer 
    (store_id, first_name, last_name, email, address_id, active, create_date)
  VALUES 
    (1, 'Test', 'User', 'testuser@example.com', 1, 1, NOW());

  -- Now use currval to get the new customer_id
  INSERT INTO rental 
    (rental_date, inventory_id, customer_id, return_date, staff_id)
  VALUES 
    (NOW(), 1, currval('customer_customer_id_seq'), NOW() + INTERVAL '7 days', 1);

COMMIT;

SELECT *
FROM customer
WHERE store_id=1;

SELECT *
FROM rental
WHERE inventory_id=1;


-- ROLLBACK;



-- 12. Simulate a failure in a multi-step transaction (update film + insert into inventory) and roll back.

BEGIN;

UPDATE film SET rental_rate = rental_rate + 1 WHERE film_id = 1;

-- Simulate failure
INSERT INTO inventory (inventory_id) VALUES (999); -- Will fail due to missing fields

ROLLBACK;


-- 13. Create a transaction that transfers an inventory item from one store to another.

SELECT *
FROM inventory
WHERE inventory_id=5;

BEGIN;

-- Let's say inventory_id 5 belongs to store_id 2
UPDATE inventory SET store_id = 1 WHERE inventory_id = 5;

COMMIT;

SELECT *
FROM inventory
WHERE inventory_id=5;


-- 14. Demonstrate SAVEPOINT and ROLLBACK TO SAVEPOINT by updating payment amounts, then undoing one.

--check original values
SELECT *
FROM payment
WHERE payment_id IN (17503,17504)


-- start the transaction
BEGIN;


-- Savepoint before changing payment 17503
SAVEPOINT sp1;
UPDATE payment SET amount = amount + 10 WHERE payment_id = 17503;
SELECT * FROM payment WHERE payment_id = 17503;



-- Savepoint before changing payment 17504
SAVEPOINT sp2;
UPDATE payment SET amount = amount + 20 WHERE payment_id = 17504;
SELECT * FROM payment WHERE payment_id = 17504;



-- Undo update to payment 17504
ROLLBACK TO sp2;




-- At this point:
-- payment_id = 17503 has been updated (+10)
-- payment_id = 17504 is UNCHANGED (rollback to sp2)

COMMIT;




-- Should reflect +10
SELECT amount FROM payment WHERE payment_id = 17503;

-- Should be original value (no change)
SELECT amount FROM payment WHERE payment_id = 17504;



-- 15. Write a transaction that deletes a customer and all associated rentals and payments, ensuring atomicity.
-- Procedure: get_overdue_rentals() that selects relevant columns.

SELECT *
FROM customer
WHERE customer_id=1;

BEGIN;

-- Replace 1 with actual customer_id
DELETE FROM payment WHERE customer_id = 1;
DELETE FROM rental WHERE customer_id = 1;
DELETE FROM customer WHERE customer_id = 1;

COMMIT;


SELECT *
FROM customer
WHERE customer_id=1;
