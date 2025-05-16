/*
Phase 6: Security & Roles

1. Create a `readonly_user` role:

   * Can run `SELECT` on `students`, `courses`, and `certificates`
   * Cannot `INSERT`, `UPDATE`, or `DELETE`

2. Create a `data_entry_user` role:

   * Can `INSERT` into `students`, `enrollments`
   * Cannot modify certificates directly
*/
---

-- 1. Create a `readonly_user` role:

--   * Can run `SELECT` on `students`, `courses`, and `certificates`
--   * Cannot `INSERT`, `UPDATE`, or `DELETE`

-- 1. Create readonly_user role with login
CREATE ROLE readonly_user LOGIN PASSWORD 'readonly123';

-- 2. Allow readonly_user to connect to the EdTech database
GRANT CONNECT ON DATABASE "EdTech" TO readonly_user;

-- 3. Grant usage on the public schema so the user can access objects inside it
GRANT USAGE ON SCHEMA public TO readonly_user;

-- 4. Grant SELECT on the required tables with schema-qualified names
GRANT SELECT ON public.students, public.courses, public.certificates TO readonly_user;

-- 5. Revoke all modifying privileges explicitly from readonly_user on all tables in the public schema
REVOKE INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA public FROM readonly_user;

-- 6. Revoke default privileges so future tables won’t grant modifying permissions accidentally
ALTER DEFAULT PRIVILEGES IN SCHEMA public REVOKE INSERT, UPDATE, DELETE ON TABLES FROM readonly_user;





-- 2. Create a `data_entry_user` role:

--   * Can `INSERT` into `students`, `enrollments`
--   * Cannot modify certificates directly



-- 1. Create the role with login
CREATE ROLE data_entry_user LOGIN PASSWORD 'dataentry123';

-- 2. Allow connection to the EdTech database
GRANT CONNECT ON DATABASE "EdTech" TO data_entry_user;

-- 3. Allow usage of the public schema
GRANT USAGE ON SCHEMA public TO data_entry_user;

-- 4. Grant INSERT privileges on students and enrollments ONLY
GRANT INSERT ON public.students, public.enrollments TO data_entry_user;

-- 5. Grant SELECT on students, enrollments, courses, trainers (optional if needed to view data)
GRANT SELECT ON public.students, public.enrollments, public.courses, public.trainers TO data_entry_user;

-- 6. Revoke all modification rights on certificates and other tables
REVOKE ALL ON public.certificates FROM data_entry_user;

-- 7. Revoke default privileges to prevent future accidental grants
ALTER DEFAULT PRIVILEGES IN SCHEMA public REVOKE ALL ON TABLES FROM data_entry_user;


GRANT USAGE ON SEQUENCE students_student_id_seq TO data_entry_user;
GRANT USAGE ON SEQUENCE enrollments_enrollment_id_seq TO data_entry_user;









SELECT *
FROM certificates;

