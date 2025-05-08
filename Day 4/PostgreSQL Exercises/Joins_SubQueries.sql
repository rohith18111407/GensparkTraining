-- Joins and SubQueries
-- https://pgexercises.com/questions/joins/

-- 1) Retrieve the start times of members' bookings
-- How can you produce a list of the start times for bookings by members named 'David Farrell'?

SELECT b.starttime
FROM cd.bookings b
JOIN cd.members m ON b.memid=m.memid
WHERE m.surname='Farrell'
AND m.firstname='David'

-- 2) Work out the start times of bookings for tennis courts
-- How can you produce a list of the start times for bookings for tennis courts, for the date '2012-09-21'? Return a list of start time and facility name pairings, ordered by the time.

SELECT b.starttime, f.name
FROM cd.bookings b
JOIN cd.facilities f ON b.facid = f.facid
WHERE f.name LIKE 'Tennis Court%' 
  AND CAST(b.starttime AS DATE) = '2012-09-21'
ORDER BY b.starttime;


-- 3) Produce a list of all members who have recommended another member
-- How can you output a list of all members who have recommended another member? Ensure that there are no duplicates in the list, and that results are ordered by (surname, firstname).

SELECT DISTINCT m.firstname, m.surname
FROM cd.members m
JOIN cd.members r ON m.memid = r.recommendedby
ORDER BY m.surname, m.firstname;


-- 4) Produce a list of all members, along with their recommender
-- How can you output a list of all members, including the individual who recommended them (if any)? Ensure that results are ordered by (surname, firstname).

SELECT m.firstname memfname,m.surname memsname, r.firstname recfname, r.surname recsname
FROM cd.members m
LEFT JOIN cd.members r ON m.recommendedby=r.memid
ORDER BY (m.surname,m.firstname);


-- 5) Produce a list of all members who have used a tennis court
-- How can you produce a list of all members who have used a tennis court? Include in your output the name of the court, and the name of the member formatted as a single column. Ensure no duplicate data, and order by the member name followed by the facility name.

SELECT DISTINCT CONCAT(m.firstname,' ',m.surname) AS member, f.name AS facility
FROM cd.members m
JOIN cd.bookings b ON m.memid=b.memid
JOIN cd.facilities f ON b.facid=f.facid
WHERE f.name LIKE 'Tennis%'
ORDER BY member,facility


-- 6) Produce a list of costly bookings
-- How can you produce a list of bookings on the day of 2012-09-14 which will cost the member (or guest) more than $30? Remember that guests have different costs to members (the listed costs are per half-hour 'slot'), and the guest user is always ID 0. Include in your output the name of the facility, the name of the member formatted as a single column, and the cost. Order by descending cost, and do not use any subqueries.

SELECT 
	 CONCAT(m.firstname, ' ', m.surname) AS member_name,
  f.name AS facility_name,
 
  CASE 
    WHEN b.memid = 0 THEN b.slots * f.guestcost
    ELSE b.slots * f.membercost
  END AS cost
FROM cd.bookings b
JOIN cd.facilities f ON b.facid = f.facid
JOIN cd.members m ON b.memid = m.memid
WHERE 
  b.starttime::date = '2012-09-14'
  AND (
    (b.memid = 0 AND b.slots * f.guestcost > 30)
    OR (b.memid <> 0 AND b.slots * f.membercost > 30)
  )
ORDER BY cost DESC;


-- 7) Produce a list of all members, along with their recommender, using no joins.
-- How can you output a list of all members, including the individual who recommended them (if any), without using any joins? Ensure that there are no duplicates in the list, and that each firstname + surname pairing is formatted as a column and ordered.

SELECT DISTINCT
  CONCAT(firstname, ' ', surname) AS member,
  (
    SELECT CONCAT(m2.firstname, ' ', m2.surname)
    FROM cd.members m2
    WHERE m2.memid = m.recommendedby
  ) AS recommender
FROM cd.members m
ORDER BY member;


-- 8) Produce a list of costly bookings, using a subquery
-- The Produce a list of costly bookings exercise contained some messy logic: we had to calculate the booking cost in both the WHERE clause and the CASE statement. Try to simplify this calculation using subqueries. For reference, the question was:
-- How can you produce a list of bookings on the day of 2012-09-14 which will cost the member (or guest) more than $30? Remember that guests have different costs to members (the listed costs are per half-hour 'slot'), and the guest user is always ID 0. Include in your output the name of the facility, the name of the member formatted as a single column, and the cost. Order by descending cost.

SELECT   
  member,
  facility,
  cost
FROM (
  SELECT 
    f.name AS facility,
    CONCAT(m.firstname, ' ', m.surname) AS member,
    CASE 
      WHEN b.memid = 0 THEN f.guestcost * b.slots
      ELSE f.membercost * b.slots
    END AS cost
  FROM cd.bookings b
  INNER JOIN cd.facilities f ON b.facid = f.facid
  INNER JOIN cd.members m ON b.memid = m.memid
  WHERE b.starttime::date = '2012-09-14'
) AS booking_costs
WHERE cost > 30
ORDER BY cost DESC;



