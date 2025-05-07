-- 1) Retrieve everything from a table

-- Question
-- How can you retrieve all the information from the cd.facilities table?

SELECT *
FROM cd.facilities;


-- 2) Retrieve specific columns from a table

-- Question
-- You want to print out a list of all of the facilities and their cost to members. How would you retrieve a list of only facility names and costs?

SELECT name,membercost
FROM cd.facilities;


-- 3) Control which rows are retrieved

-- Question
-- How can you produce a list of facilities that charge a fee to members?

SELECT *
FROM cd.facilities
WHERE membercost>0;


-- 4) Control which rows are retrieved - part 2

-- Question
-- How can you produce a list of facilities that charge a fee to members, and that fee is less than 1/50th of the monthly maintenance cost? Return the facid, facility name, member cost, and monthly maintenance of the facilities in question.

SELECT facid,name,membercost,monthlymaintenance
FROM cd.facilities
WHERE membercost>0 AND membercost<(0.02*(monthlymaintenance));


-- 5) Basic string searches

-- Question
-- How can you produce a list of all facilities with the word 'Tennis' in their name?

SELECT *
FROM cd.facilities
WHERE name LIKE '%Tennis%';


-- 6) Matching against multiple possible values

-- Question
-- How can you retrieve the details of facilities with ID 1 and 5? Try to do it without using the OR operator.

SELECT *
FROM cd.facilities
WHERE facid IN (1,5);


-- 7) Classify results into buckets

-- Question
-- How can you produce a list of facilities, with each labelled as 'cheap' or 'expensive' depending on if their monthly maintenance cost is more than $100? Return the name and monthly maintenance of the facilities in question.

SELECT name, 
	CASE
		WHEN monthlymaintenance>100 THEN 'expensive'
		ELSE 'cheap'
	END
	AS cost
FROM cd.facilities;

-- 8) Working with dates

-- Question
-- How can you produce a list of members who joined after the start of September 2012? Return the memid, surname, firstname, and joindate of the members in question.


SELECT memid,surname,firstname,joindate
FROM cd.members
WHERE joindate> '2012-09-01';


-- 9) Removing duplicates, and ordering results

-- Question
-- How can you produce an ordered list of the first 10 surnames in the members table? The list must not contain duplicates.

SELECT DISTINCT surname
FROM cd.members
ORDER BY surname
LIMIT 10;


-- 10) Combining results from multiple queries

-- Question
-- You, for some reason, want a combined list of all surnames and all facility names. Yes, this is a contrived example :-). Produce that list!


SELECT surname
FROM cd.members
UNION
SELECT name AS surname
FROM cd.facilities;


-- 11) Simple aggregation

-- Question
-- You'd like to get the signup date of your last member. How can you retrieve this information?

SELECT MAX(joindate) latest
FROM cd.members;


-- 12) More aggregation

-- Question
-- You'd like to get the first and last name of the last member(s) who signed up - not just the date. How can you do that?

SELECT firstname, surname, joindate
FROM cd.members
WHERE joindate = (
  SELECT MAX(joindate)
  FROM cd.members
);


