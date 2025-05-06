--  Drop all tables if exist
-- Drop dependent tables first
DROP TABLE IF EXISTS SALES;
DROP TABLE IF EXISTS ITEM;

-- Drop foreign key constraints referencing EMP and DEPARTMENT
ALTER TABLE DEPARTMENT DROP CONSTRAINT FK_DEPARTMENT_empno;
ALTER TABLE EMP DROP CONSTRAINT FK_EMP_deptname;
ALTER TABLE EMP DROP CONSTRAINT FK_EMP_bossno;

-- Now drop the remaining tables
DROP TABLE IF EXISTS DEPARTMENT;
DROP TABLE IF EXISTS EMP;


--  Recreate EMP without FK on deptname for now
CREATE TABLE EMP (
    empno INT PRIMARY KEY,
    empname VARCHAR(100),
    salary DECIMAL(10, 2),
    deptname VARCHAR(100) NULL,  -- Will update later
    bossno INT NULL,
    CONSTRAINT FK_EMP_bossno FOREIGN KEY (bossno) REFERENCES EMP(empno)
);

-- Recreate DEPARTMENT
CREATE TABLE DEPARTMENT (
    deptname VARCHAR(100) PRIMARY KEY,
    floor INT,
    phone VARCHAR(20),
    empno INT NOT NULL,
    CONSTRAINT FK_DEPARTMENT_empno FOREIGN KEY (empno) REFERENCES EMP(empno)
);

-- Insert EMP records with deptname = NULL
INSERT INTO EMP (empno, empname, salary, deptname, bossno) VALUES
(1, 'Alice', 75000, NULL, NULL),
(2, 'Ned', 45000, NULL, 1),
(3, 'Andrew', 25000, NULL, 2),
(4, 'Clare', 22000, NULL, 2),
(5, 'Todd', 38000, NULL, 1),
(6, 'Nancy', 22000, NULL, 5),
(7, 'Brier', 43000, NULL, 1),
(8, 'Sarah', 56000, NULL, 7),
(9, 'Sophile', 35000, NULL, 1),
(10, 'Sanjay', 15000, NULL, 3),
(11, 'Rita', 15000, NULL, 4),
(12, 'Gigi', 16000, NULL, 4),
(13, 'Maggie', 11000, NULL, 4),
(14, 'Paul', 15000, NULL, 3),
(15, 'James', 15000, NULL, 3),
(16, 'Pat', 15000, NULL, 3),
(17, 'Mark', 15000, NULL, 3);

-- Insert DEPARTMENT records
INSERT INTO DEPARTMENT VALUES
('Management', 5, '34', 1),
('Books', 1, '81', 4),
('Clothes', 2, '24', 4),
('Equipment', 3, '57', 3),
('Furniture', 4, '14', 3),
('Navigation', 1, '41', 3),
('Recreation', 2, '29', 4),
('Accounting', 5, '35', 5),
('Purchasing', 5, '36', 7),
('Personnel', 5, '37', 9),
('Marketing', 5, '38', 2);


-- Now update EMP with correct deptname
UPDATE EMP SET deptname = 'Management' WHERE empno = 1;
UPDATE EMP SET deptname = 'Marketing' WHERE empno IN (2,3,4);
UPDATE EMP SET deptname = 'Accounting' WHERE empno IN (5,6);
UPDATE EMP SET deptname = 'Purchasing' WHERE empno IN (7,8);
UPDATE EMP SET deptname = 'Personnel' WHERE empno = 9;
UPDATE EMP SET deptname = 'Navigation' WHERE empno = 10;
UPDATE EMP SET deptname = 'Books' WHERE empno = 11;
UPDATE EMP SET deptname = 'Clothes' WHERE empno IN (12,13);
UPDATE EMP SET deptname = 'Equipment' WHERE empno IN (14,15);
UPDATE EMP SET deptname = 'Furniture' WHERE empno = 16;
UPDATE EMP SET deptname = 'Recreation' WHERE empno = 17;

-- Add the foreign key after data is valid
ALTER TABLE EMP
ADD CONSTRAINT FK_EMP_deptname FOREIGN KEY (deptname) REFERENCES DEPARTMENT(deptname);

-- Create ITEM and SALES as before
CREATE TABLE ITEM (
    itemname VARCHAR(100) PRIMARY KEY,
    itemtype CHAR(1),
    itemcolor VARCHAR(50)
);

CREATE TABLE SALES (
    salesno INT PRIMARY KEY,
    saleqty INT,
    itemname VARCHAR(100) NOT NULL,
    deptname VARCHAR(100) NOT NULL,
    CONSTRAINT FK_SALES_itemname FOREIGN KEY (itemname) REFERENCES ITEM(itemname),
    CONSTRAINT FK_SALES_deptname FOREIGN KEY (deptname) REFERENCES DEPARTMENT(deptname)
);

-- Insert into ITEM
INSERT INTO ITEM VALUES
('Pocket Knife-Nile', 'E', 'Brown'),
('Pocket Knife-Avon', 'E', 'Brown'),
('Compass', 'N', NULL),
('Geo positioning system', 'N', NULL),
('Elephant Polo stick', 'R', 'Bamboo'),
('Camel Saddle', 'R', 'Brown'),
('Sextant', 'N', NULL),
('Map Measure', 'N', NULL),
('Boots-snake proof', 'C', 'Green'),
('Pith Helmet', 'C', 'Khaki'),
('Hat-polar Explorer', 'C', 'White'),
('Exploring in 10 Easy Lessons', 'B', NULL),
('Hammock', 'F', 'Khaki'),
('How to win Foreign Friends', 'B', NULL),
('Map case', 'E', 'Brown'),
('Safari Chair', 'F', 'Khaki'),
('Safari cooking kit', 'F', 'Khaki'),
('Stetson', 'C', 'Black'),
('Tent - 2 person', 'F', 'Khaki'),
('Tent -8 person', 'F', 'Khaki');

--  Insert into SALES
INSERT INTO SALES VALUES
(101, 2, 'Boots-snake proof', 'Clothes'),
(102, 1, 'Pith Helmet', 'Clothes'),
(103, 1, 'Sextant', 'Navigation'),
(104, 3, 'Hat-polar Explorer', 'Clothes'),
(105, 5, 'Pith Helmet', 'Equipment'),
(106, 2, 'Pocket Knife-Nile', 'Clothes'),
(107, 3, 'Pocket Knife-Nile', 'Recreation'),
(108, 1, 'Compass', 'Navigation'),
(109, 2, 'Geo positioning system', 'Navigation'),
(110, 5, 'Map Measure', 'Navigation'),
(111, 1, 'Geo positioning system', 'Books'),
(112, 1, 'Sextant', 'Books'),
(113, 3, 'Pocket Knife-Nile', 'Books'),
(114, 1, 'Pocket Knife-Nile', 'Navigation'),
(115, 1, 'Pocket Knife-Nile', 'Equipment'),
(116, 1, 'Sextant', 'Clothes'),
(117, 1, 'Sextant', 'Equipment'),
(118, 1, 'Sextant', 'Recreation'),
(119, 1, 'Sextant', 'Furniture'),
(120, 1, 'Pocket Knife-Nile', 'Furniture'),
(121, 1, 'Exploring in 10 Easy Lessons', 'Books'),
(122, 1, 'How to win Foreign Friends', 'Books'),
(123, 1, 'Compass', 'Books'),
(124, 1, 'Pith Helmet', 'Books'),
(125, 1, 'Elephant Polo stick', 'Recreation'),
(126, 1, 'Camel Saddle', 'Recreation');


SELECT *
FROM EMP;

SELECT *
FROM DEPARTMENT;

SELECT *
FROM SALES;

SELECT *
FROM ITEM;
