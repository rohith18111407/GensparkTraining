/*
Phase 5: Cursor

Use a cursor to:

* Loop through all students in a course
* Print name and email of those who do not yet have certificates

*/
---

DO
$$
DECLARE 
	r RECORD;
	cur CURSOR FOR
		SELECT s.name, s.email
        FROM students s
        JOIN enrollments e ON s.student_id = e.student_id
        LEFT JOIN certificates c ON c.enrollment_id = e.enrollment_id
        WHERE e.course_id = 1 AND c.enrollment_id IS NULL;
BEGIN
	OPEN cur;
	LOOP 
		FETCH cur INTO r;
		EXIT WHEN NOT FOUND;

		RAISE NOTICE 'Student without certificate: %, %', r.name, r.email;
	END LOOP;
	CLOSE cur;
END;
$$;
















