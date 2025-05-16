/*
Tables to Design (Normalized to 3NF):

1. **students**

   * `student_id (PK)`, `name`, `email`, `phone`

2. **courses**

   * `course_id (PK)`, `course_name`, `category`, `duration_days`

3. **trainers**

   * `trainer_id (PK)`, `trainer_name`, `expertise`

4. **enrollments**

   * `enrollment_id (PK)`, `student_id (FK)`, `course_id (FK)`, `enroll_date`

5. **certificates**

   * `certificate_id (PK)`, `enrollment_id (FK)`, `issue_date`, `serial_no`

6. **course\_trainers** (Many-to-Many if needed)

   * `course_id`, `trainer_id`
*/
---
/*
Phase 2: DDL & DML

* Create all tables with appropriate constraints (PK, FK, UNIQUE, NOT NULL)
* Insert sample data using `INSERT` statements
* Create indexes on `student_id`, `email`, and `course_id`
*/
---

DROP TABLE IF EXISTS certificates, enrollments, course_trainers, courses, trainers, students CASCADE;

CREATE TABLE students (
    student_id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    email TEXT UNIQUE NOT NULL,
    phone TEXT UNIQUE
);

CREATE TABLE courses (
    course_id SERIAL PRIMARY KEY,
    course_name TEXT NOT NULL,
    category TEXT,
    duration_days INT
);

CREATE TABLE trainers (
    trainer_id SERIAL PRIMARY KEY,
    trainer_name TEXT NOT NULL,
    expertise TEXT
);

CREATE TABLE enrollments (
    enrollment_id SERIAL PRIMARY KEY,
    student_id INT REFERENCES students(student_id),
    course_id INT REFERENCES courses(course_id),
    enroll_date DATE DEFAULT CURRENT_DATE
);

CREATE TABLE certificates (
    certificate_id SERIAL PRIMARY KEY,
    enrollment_id INT UNIQUE REFERENCES enrollments(enrollment_id),
    issue_date DATE DEFAULT CURRENT_DATE,
    serial_no TEXT UNIQUE NOT NULL
);

CREATE TABLE course_trainers (
    course_id INT REFERENCES courses(course_id),
    trainer_id INT REFERENCES trainers(trainer_id),
    PRIMARY KEY (course_id, trainer_id)
);




INSERT INTO students (name, email, phone) VALUES 
('Sachin Tendulkar', 'sachintendulkar@gmail.com', '9999988888'),
('Mahendra Singh Dhoni', 'msdhoni@gmail.com', '8888877777');

INSERT INTO courses (course_name, category, duration_days) VALUES 
('Python Basics', 'Programming', 30),
('Data Science', 'AI', 60);

INSERT INTO trainers (trainer_name, expertise) VALUES 
('Rohit Sharma', 'Python'),
('Virat Kohli', 'Data Science');

INSERT INTO course_trainers VALUES (1, 1), (2, 2);

INSERT INTO enrollments (student_id, course_id) VALUES 
(1, 1), (2, 2);

INSERT INTO certificates (enrollment_id, serial_no) VALUES 
(1, 'CERT-001'), (2, 'CERT-002');




SELECT *
FROM students;


SELECT *
FROM courses;


SELECT *
FROM trainers;


SELECT *
FROM enrollments;


SELECT *
FROM certificates;


SELECT *
FROM course_trainers;




CREATE INDEX idx_student_id ON students(student_id);
CREATE INDEX idx_email ON students(email);
CREATE INDEX idx_course_id ON courses(course_id);






