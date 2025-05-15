/*
1. Create a stored procedure to encrypt a given text
Task: Write a stored procedure sp_encrypt_text that takes a plain text input (e.g., email or mobile number) and returns an encrypted version using PostgreSQL's pgcrypto extension.
 
Use pgp_sym_encrypt(text, key) from pgcrypto.
 
2. Create a stored procedure to compare two encrypted texts
Task: Write a procedure sp_compare_encrypted that takes two encrypted values and checks if they decrypt to the same plain text.
 
3. Create a stored procedure to partially mask a given text
Task: Write a procedure sp_mask_text that:
 
Shows only the first 2 and last 2 characters of the input string
 
Masks the rest with *
 
E.g., input: 'john.doe@example.com' → output: 'jo***************om'
 
4. Create a procedure to insert into customer with encrypted email and masked name
Task:
 
Call sp_encrypt_text for email
 
Call sp_mask_text for first_name
 
Insert masked and encrypted values into the customer table
 
Use any valid address_id and store_id to satisfy FK constraints.
 
5. Create a procedure to fetch and display masked first_name and decrypted email for all customers
Task:
Write sp_read_customer_masked() that:
 
Loops through all rows
 
Decrypts email
 
Displays customer_id, masked first name, and decrypted email

*/


--  Enable pgcrypto Extension
-- allows access to encryption functions like pgp_sym_encrypt and pgp_sym_decrypt

CREATE EXTENSION IF NOT EXISTS pgcrypto;

-- customer Table

CREATE TABLE customer (
    customer_id SERIAL PRIMARY KEY,
    store_id INT,
    first_name TEXT,
    last_name TEXT,
    email TEXT,
    address_id INT
);

/*
1. Create a stored procedure to encrypt a given text
Task: Write a stored procedure sp_encrypt_text that takes a plain text input (e.g., email or mobile number) and returns an encrypted version using PostgreSQL's pgcrypto extension.
 
Use pgp_sym_encrypt(text, key) from pgcrypto.
 
*/


CREATE OR REPLACE PROCEDURE sp_encrypt_text_proc(
    IN p_text TEXT,
    OUT p_encrypted TEXT
)
LANGUAGE plpgsql
AS $$
DECLARE
    secret_key TEXT := 'ROHITH_secret_key';
BEGIN
    p_encrypted := pgp_sym_encrypt(p_text, secret_key);
END;
$$;


DO $$
DECLARE
    encrypted_result TEXT;
BEGIN
    CALL sp_encrypt_text_proc('rohith@gmail.com', encrypted_result);
    RAISE NOTICE 'Encrypted Text: %', encrypted_result;
END;
$$;




/*
2. Create a stored procedure to compare two encrypted texts
Task: Write a procedure sp_compare_encrypted that takes two encrypted values and checks if they decrypt to the same plain text.
*/


CREATE OR REPLACE PROCEDURE sp_compare_encrypted_proc(
    IN p_enc1 TEXT,
    IN p_enc2 TEXT,
    OUT is_equal BOOLEAN
)
LANGUAGE plpgsql
AS $$
DECLARE
    secret_key TEXT := 'ROHITH_secret_key';
    plain1 TEXT;
    plain2 TEXT;
BEGIN
    plain1 := pgp_sym_decrypt(p_enc1::bytea, secret_key);
    plain2 := pgp_sym_decrypt(p_enc2::bytea, secret_key);
    is_equal := (plain1 = plain2);
END;
$$;


DO $$
DECLARE
    e1 TEXT;
    e2 TEXT;
    result BOOLEAN;
BEGIN
    CALL sp_encrypt_text_proc('12345', e1);
    CALL sp_encrypt_text_proc('12345', e2);

    CALL sp_compare_encrypted_proc(e1, e2, result);
    RAISE NOTICE 'Match: %', result;
END;
$$;


/*
3. Create a stored procedure to partially mask a given text
Task: Write a procedure sp_mask_text that:
 
Shows only the first 2 and last 2 characters of the input string
 
Masks the rest with *
 
E.g., input: 'john.doe@example.com' → output: 'jo***************om'
 
*/


CREATE OR REPLACE PROCEDURE sp_mask_text_proc(
    IN p_text TEXT,
    OUT p_masked TEXT
)
LANGUAGE plpgsql
AS $$
DECLARE
    len INT := LENGTH(p_text);
BEGIN
    IF len <= 4 THEN
        p_masked := repeat('*', len);
    ELSE
        p_masked := substr(p_text, 1, 2) || repeat('*', len - 4) || substr(p_text, len - 1, 2);
    END IF;
END;
$$;


DO $$
DECLARE
    masked_result TEXT;
BEGIN
    CALL sp_mask_text_proc('shivam.dube@gmail.com', masked_result);
    RAISE NOTICE 'Masked Text: %', masked_result; 
END;
$$;



/*
4. Create a procedure to insert into customer with encrypted email and masked name
Task:
 
Call sp_encrypt_text for email
 
Call sp_mask_text for first_name
 
Insert masked and encrypted values into the customer table
 
Use any valid address_id and store_id to satisfy FK constraints.
 
*/


CREATE OR REPLACE PROCEDURE sp_insert_customer_encrypted_proc(
    IN p_store_id INT,
    IN p_first_name TEXT,
    IN p_last_name TEXT,
    IN p_email TEXT,
    IN p_address_id INT
)
LANGUAGE plpgsql
AS $$
DECLARE
    encrypted_email TEXT;
    masked_first_name TEXT;
BEGIN
    CALL sp_encrypt_text_proc(p_email, encrypted_email);
    
    CALL sp_mask_text_proc(p_first_name, masked_first_name);
    
    INSERT INTO customer (store_id, first_name, last_name, email, address_id)
    VALUES (p_store_id, masked_first_name, p_last_name, encrypted_email, p_address_id);
    
    RAISE NOTICE 'Customer inserted: %, %, %', masked_first_name, p_last_name, encrypted_email;
END;
$$;


CALL sp_insert_customer_encrypted_proc(
    1, 'Shivam Dube', 'Cricketer', 'shivam.dube@gmail.com', 1
);


SELECT * FROM customer;



/*
5. Create a procedure to fetch and display masked first_name and decrypted email for all customers
Task:
Write sp_read_customer_masked() that:
 
Loops through all rows
 
Decrypts email
 
Displays customer_id, masked first name, and decrypted email
*/



CREATE OR REPLACE PROCEDURE sp_read_customer_masked_proc()
LANGUAGE plpgsql
AS $$
DECLARE
    r RECORD;
    decrypted_email TEXT;
    secret_key TEXT := 'ROHITH_secret_key';
BEGIN
    FOR r IN SELECT * FROM customer LOOP
        decrypted_email := pgp_sym_decrypt(r.email::bytea, secret_key);
        
        RAISE NOTICE 'ID: %, Masked Name: %, Email: %',
            r.customer_id, r.first_name, decrypted_email;
    END LOOP;
END;
$$;


CALL sp_read_customer_masked_proc();




