/*
Phase 7: Transactions & Atomicity

Write a transaction block that:

* Enrolls a student
* Issues a certificate
* Fails if certificate generation fails (rollback)

*/
```sql
BEGIN;
-- insert into enrollments
-- insert into certificates
-- COMMIT or ROLLBACK on error
```
---

SELECT * FROM enrollments WHERE student_id = 2 AND course_id = 1;
SELECT * FROM certificates WHERE serial_no = 'CERT-004';


-- Successful Transaction Block

BEGIN;
-- Insert into enrollments
WITH inserted_enrollment AS (
  INSERT INTO enrollments (student_id, course_id)
  VALUES (2, 1)  -- Mahendra Singh Dhoni into Python Basics
  RETURNING enrollment_id
)
-- Insert into certificates
INSERT INTO certificates (enrollment_id, serial_no)
SELECT enrollment_id, 'CERT-007'
FROM inserted_enrollment;
COMMIT;


SELECT * FROM enrollments WHERE student_id = 2 AND course_id = 1;
SELECT * FROM certificates WHERE serial_no = 'CERT-007';


-- Failing Transaction → Rollback

BEGIN;
-- Insert a new enrollment (duplicate)
WITH inserted_enrollment AS (
  INSERT INTO enrollments (student_id, course_id)
  VALUES (2, 2)  -- Re-enroll in Data Science
  RETURNING enrollment_id
)
--inserting a certificate with an existing serial_no (CERT-002)
INSERT INTO certificates (enrollment_id, serial_no)
SELECT enrollment_id, 'CERT-002'  -- Duplicate
FROM inserted_enrollment;
-- will fail
COMMIT;

ROLLBACK;

SELECT * FROM enrollments WHERE student_id = 2 AND course_id = 2 ORDER BY enrollment_id DESC;
