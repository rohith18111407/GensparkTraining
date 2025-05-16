/*
Phase 4: Functions & Stored Procedures

Function:

Create `get_certified_students(course_id INT)`
→ Returns a list of students who completed the given course and received certificates.

Stored Procedure:

Create `sp_enroll_student(p_student_id, p_course_id)`
→ Inserts into `enrollments` and conditionally adds a certificate if completed (simulate with status flag).
*/

---

-- Function:

-- Create `get_certified_students(course_id INT)`
-- Returns a list of students who completed the given course and received certificates.

CREATE OR REPLACE FUNCTION get_certified_students(p_course_id INT)
RETURNS TABLE(student_name TEXT, email TEXT)
AS $$
BEGIN
	RETURN QUERY
	SELECT s.name,s.email
	FROM students s
	JOIN enrollments e ON s.student_id = e.student_id
	JOIN certificates c on c.enrollment_id = e.enrollment_id
	WHERE e.course_id = p_course_id;
END;
$$ LANGUAGE plpgsql;



SELECT get_certified_students(1);



-- Stored Procedure:
-- Create `sp_enroll_student(p_student_id, p_course_id)`
-- → Inserts into `enrollments` and conditionally adds a certificate if completed (simulate with status flag).

SELECT * FROM enrollments;
SELECT * FROM certificates;


CREATE OR REPLACE PROCEDURE sp_enroll_student(p_student_id INT, p_course_id INT, p_completed BOOLEAN)
LANGUAGE plpgsql
AS $$
DECLARE
	new_enrollment_id INT;
BEGIN
	INSERT INTO enrollments (student_id, course_id)
    VALUES (p_student_id, p_course_id)
    RETURNING enrollment_id INTO new_enrollment_id;

	 IF p_completed THEN
        INSERT INTO certificates (enrollment_id, serial_no)
        VALUES (new_enrollment_id, 'CERT-' || LPAD(new_enrollment_id::TEXT, 3, '0'));
    END IF;
END;
$$;

-- Case 1: Enroll new student who completed
CALL sp_enroll_student(4, 1, TRUE);

-- Case 2: Enroll new student who hasn't completed
CALL sp_enroll_student(2, 1, FALSE);


SELECT * FROM enrollments;
SELECT * FROM certificates;

SELECT *
FROM students;

