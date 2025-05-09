-- SELECT Queries
-- 1. List all films with their length and rental rate, sorted by length descending.
-- Columns: title, length, rental_rate

SELECT title, length, rental_rate
FROM film
ORDER BY length DESC;




-- 2. Find the top 5 customers who have rented the most films.
-- Hint: Use the rental and customer tables.

SELECT c.first_name, c.last_name, COUNT(r.rental_id) AS rental_count
FROM customer c
JOIN rental r ON c.customer_id = r.customer_id
GROUP BY c.customer_id
ORDER BY rental_count DESC
LIMIT 5;




-- 3. Display all films that have never been rented.
-- Hint: Use LEFT JOIN between film and inventory → rental.

SELECT f.title
FROM film f
LEFT JOIN inventory i ON f.film_id = i.film_id
LEFT JOIN rental r ON i.inventory_id = r.inventory_id
WHERE r.rental_id IS NULL;



-- JOIN Queries
-- 4. List all actors who appeared in the film ‘Academy Dinosaur’.
-- Tables: film, film_actor, actor

SELECT a.first_name, a.last_name
FROM actor a
JOIN film_actor fa ON a.actor_id = fa.actor_id
JOIN film f ON fa.film_id = f.film_id
WHERE f.title = 'Academy Dinosaur';


-- 5. List each customer along with the total number of rentals they made and the total amount paid.
-- Tables: customer, rental, payment

SELECT c.first_name, c.last_name,
       COUNT(r.rental_id) AS total_rentals,
       SUM(p.amount) AS total_paid
FROM customer c
LEFT JOIN rental r ON c.customer_id = r.customer_id
LEFT JOIN payment p ON c.customer_id = p.customer_id
GROUP BY c.customer_id;


-- CTE-Based Queries
-- 6. Using a CTE, show the top 3 rented movies by number of rentals.
-- Columns: title, rental_count

WITH cte_rental_counts AS (
    SELECT f.title, COUNT(r.rental_id) AS rental_count
    FROM film f
    JOIN inventory i ON f.film_id = i.film_id
    JOIN rental r ON i.inventory_id = r.inventory_id
    GROUP BY f.title
)
SELECT title, rental_count
FROM cte_rental_counts
ORDER BY rental_count DESC
LIMIT 3;



-- 7. Find customers who have rented more than the average number of films.
-- Use a CTE to compute the average rentals per customer, then filter.

WITH cte_customer_rentals AS (
    SELECT customer_id, COUNT(rental_id) AS rental_count
    FROM rental
    GROUP BY customer_id
),
cte_average_rentals AS (
    SELECT AVG(rental_count) AS avg_rentals
    FROM cte_customer_rentals
)
SELECT c.first_name, c.last_name, cr.rental_count
FROM cte_customer_rentals cr
JOIN customer c ON cr.customer_id = c.customer_id
JOIN cte_average_rentals ar ON cr.rental_count > ar.avg_rentals;



-- Function Questions
-- 8. Write a function that returns the total number of rentals for a given customer ID.
-- Function: get_total_rentals(customer_id INT)

SELECT DISTINCT customer_id
FROM rental;

-- DROP FUNCTION get_total_rentals;

CREATE OR REPLACE FUNCTION get_total_rentals(p_customer_id INT)
RETURNS INT AS $$
BEGIN
    RETURN (
        SELECT COUNT(*)
        FROM rental
        WHERE customer_id = p_customer_id
    );
END;
$$ LANGUAGE plpgsql;




SELECT get_total_rentals(1);






-- Stored Procedure Questions
-- 9. Write a stored procedure that updates the rental rate of a film by film ID and new rate.
-- Procedure: update_rental_rate(film_id INT, new_rate NUMERIC)

SELECT DISTINCT film_id,rental_rate
FROM film
ORDER BY film_id;

CREATE OR REPLACE PROCEDURE update_rental_rate(p_film_id INT, p_new_rate NUMERIC)
LANGUAGE plpgsql
AS $$
BEGIN
    UPDATE film
    SET rental_rate = p_new_rate
    WHERE film_id = p_film_id;
END;
$$;


CALL update_rental_rate(10, 6.00);

SELECT DISTINCT film_id,rental_rate
FROM film
ORDER BY film_id;

-- 10. Write a procedure to list overdue rentals (return date is NULL and rental date older than 7 days).
-- Procedure: get_overdue_rentals() that selects relevant columns.

CREATE OR REPLACE PROCEDURE get_overdue_rentals()
LANGUAGE SQL
AS $$
    SELECT r.rental_id, c.first_name, c.last_name, f.title, r.rental_date
    FROM rental r
    JOIN inventory i ON r.inventory_id = i.inventory_id
    JOIN film f ON i.film_id = f.film_id
    JOIN customer c ON r.customer_id = c.customer_id
    WHERE r.return_date IS NULL AND r.rental_date < NOW() - INTERVAL '7 days';
$$;


CALL get_overdue_rentals();

DROP PROCEDURE get_overdue_rentals

CREATE OR REPLACE FUNCTION get_overdue_rentals()
RETURNS TABLE (
    rental_id INT,
    first_name TEXT,
    last_name TEXT,
    title TEXT,
    rental_date TIMESTAMP
)
LANGUAGE SQL
AS $$
    SELECT r.rental_id, c.first_name, c.last_name, f.title, r.rental_date
    FROM rental r
    JOIN inventory i ON r.inventory_id = i.inventory_id
    JOIN film f ON i.film_id = f.film_id
    JOIN customer c ON r.customer_id = c.customer_id
    WHERE r.return_date IS NULL AND r.rental_date < NOW() - INTERVAL '7 days';
$$;


SELECT * FROM get_overdue_rentals();






