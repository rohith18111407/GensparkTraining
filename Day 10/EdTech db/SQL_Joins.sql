/*
Phase 3: SQL Joins Practice

Write queries to:

1. List students and the courses they enrolled in
2. Show students who received certificates with trainer names
3. Count number of students per course
*/
---

-- 1. List students and the courses they enrolled in

SELECT s.name AS student_name, c.course_name
FROM students s
JOIN enrollments e ON s.student_id = e.student_id
JOIN courses c ON e.course_id = c.course_id;



-- 2. Show students who received certificates with trainer names

SELECT s.name AS student, c.course_name, t.trainer_name, cert.serial_no
FROM students s
JOIN enrollments e ON s.student_id = e.student_id
JOIN certificates cert ON cert.enrollment_id = e.enrollment_id
JOIN courses c ON c.course_id = e.course_id
JOIN course_trainers ct ON ct.course_id = c.course_id
JOIN trainers t ON t.trainer_id = ct.trainer_id;



-- 3. Count number of students per course

SELECT c.course_name, COUNT(e.student_id) AS student_count
FROM courses c
LEFT JOIN enrollments e ON c.course_id = e.course_id
GROUP BY c.course_name;




