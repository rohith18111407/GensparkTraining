-- Example - 1
-- Use of cursor to print the values


DO $$
DECLARE
    rental_record RECORD;
    rental_cursor CURSOR FOR
        SELECT r.rental_id, c.first_name, c.last_name, r.rental_date
        FROM rental r
        JOIN customer c ON r.customer_id = c.customer_id
        ORDER BY r.rental_id;
BEGIN
    OPEN rental_cursor;

    LOOP
        FETCH rental_cursor INTO rental_record;
        EXIT WHEN NOT FOUND;

        RAISE NOTICE 'rental id: %, customer: % %, date: %',
                     rental_record.rental_id,
                     rental_record.first_name,
                     rental_record.last_name,
                     rental_record.rental_date;
    END LOOP;

    CLOSE rental_cursor;
END;
$$






-- Example-2
-- Use of cursor to insert the data into new table

CREATE TABLE rental_tax_log (
    rental_id int,
    customer_name text,
    rental_date timestamp,
    amount numeric,
    tax numeric
);


DO $$
DECLARE
    rec RECORD;
    cur CURSOR FOR
        SELECT r.rental_id, 
               c.first_name || ' ' || c.last_name AS customer_name,
               r.rental_date,
               p.amount
        FROM rental r
        JOIN payment p ON r.rental_id = p.rental_id
        JOIN customer c ON r.customer_id = c.customer_id;
BEGIN
    OPEN cur;

    LOOP
        FETCH cur INTO rec;
        EXIT WHEN NOT FOUND;

        INSERT INTO rental_tax_log (rental_id, customer_name, rental_date, amount, tax)
        VALUES (
            rec.rental_id,
            rec.customer_name,
            rec.rental_date,
            rec.amount,
            rec.amount * 0.10
        );
    END LOOP;

    CLOSE cur;
END;
$$;


SELECT *
FROM rental_tax_log;





