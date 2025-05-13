DROP TABLE IF EXISTS audit_log;

CREATE TABLE audit_log
(
	audit_id SERIAL PRIMARY KEY,
	table_name TEXT,
	fieldname TEXT,
	old_value TEXT,
	new_value TEXT,
	updated_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);


-- Store the old_value and new_value in audit_log

CREATE OR REPLACE FUNCTION Update_Audit_log()
RETURNS TRIGGER
AS $$
BEGIN
	INSERT INTO audit_log(table_name, fieldname,old_value,new_value, updated_date)
	VALUES ('customer','email',OLD.email,NEW.email, CURRENT_TIMESTAMP);
	RETURN NEW;
END;
$$ LANGUAGE plpgsql


CREATE OR REPLACE TRIGGER trg_log_customer_email_Change
BEFORE UPDATE
	ON customer
FOR EACH ROW
	EXECUTE FUNCTION Update_Audit_log();


SELECT *
FROM customer;

SELECT *
FROM customer
WHERE customer_id = 2;


UPDATE customer 
SET email='mary.smith.new@gmail.com'
WHERE customer_id = 2;

SELECT *
FROM audit_log;

SELECT *
FROM customer
WHERE customer_id = 2;

--------------------------------------------------------------------
-- Store old_value and new_value as JSON in audit_log

SELECT *
FROM audit_log;


CREATE OR REPLACE FUNCTION Update_Audit_log()
RETURNS TRIGGER
AS $$
DECLARE
	col_name TEXT := TG_ARGV[0];
	tab_name TEXT := TG_ARGV[1];
	o_value TEXT;
	n_value TEXT;
BEGIN
	o_value := ROW_TO_JSON(OLD);
	n_value := ROW_TO_JSON(NEW);

	IF o_value IS DISTINCT FROM n_value THEN 
		INSERT INTO audit_log(table_name, fieldname,old_value,new_value, updated_date)
		VALUES (tab_name,col_name,o_value,n_value, CURRENT_TIMESTAMP);
	END IF;
	RETURN NEW;
	
END;
$$ LANGUAGE plpgsql


CREATE OR REPLACE TRIGGER trg_log_customer_email_Change
AFTER UPDATE 
	ON customer
FOR EACH ROW
	EXECUTE FUNcTION Update_Audit_log('email','customer');



SELECT *
FROM customer;

SELECT *
FROM customer
WHERE customer_id = 2;


UPDATE customer 
SET email='newnew.mary.smith.new@gmail.com'
WHERE customer_id = 2;

SELECT *
FROM audit_log;

SELECT *
FROM customer
WHERE customer_id = 2;




-----------------------------------------------------------------------------------------


CREATE OR REPLACE FUNCTION Update_Audit_log()
RETURNS TRIGGER 
AS $$
DECLARE 
   col_name text := TG_ARGV[0];
   tab_name text := TG_ARGV[1];
   o_value text;
   n_value text;
BEGIN
    EXECUTE FORMAT('select ($1).%I::TEXT', COL_NAME) INTO O_VALUE USING OLD; -- %I is the placeholder
    EXECUTE FORMAT('select ($1).%I::TEXT', COL_NAME) INTO N_VALUE USING NEW;
	IF o_value IS DISTINCT FROM n_value THEN
		INSERT INTO audit_log(table_name,fieldname,old_value,new_value,updated_date) 
		VALUES(tab_name,col_name,o_value,n_value,current_Timestamp);
	END IF;
	RETURN NEW;
END;
$$ language plpgsql



CREATE OR REPLACE TRIGGER trg_log_customer_email_Change
AFTER UPDATE
	ON customer
FOR EACH ROW
	EXECUTE FUNCTION Update_Audit_log('last_name','customer');

SELECT *
FROM customer
WHERE customer_id=2;


UPDATE customer 
SET last_name = 'Smith' 
WHERE customer_id = 2;

SELECT *
FROM audit_log;

SELECT *
FROM customer
WHERE customer_id=2;









